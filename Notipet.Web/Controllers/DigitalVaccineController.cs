using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Domain;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalVaccineController : ControllerBase
    {
        private readonly NotiPetBdContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        public DigitalVaccineController(NotiPetBdContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }


        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostDigitalVaccine(DigitalVaccineDto digitalVaccine)
        {
            try
            {
                var vaccine = digitalVaccine.ConvertToType();
                _context.DigitalVaccines.Add(vaccine);
                await _context.SaveChangesAsync();

                return Ok(new JsendSuccess(new
                {
                    vaccine = vaccine
                }));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpGet("ByBusinessId/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> GetDigitalVaccineByBusinessId(int businessId, int itemCount, int page)
        {
            try
            {
                var vaccines = await _context.DigitalVaccines
                    .Where(x => x.BusinessId == businessId)
                    .Include("Vaccine")
                    .Include(x => x.Pet)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();
                var pagination = new PaginationInfo(itemCount, page, vaccines.Count);
                vaccines = vaccines.Skip(pagination.StartAt).Take(pagination.ItemCount).ToList();
                return Ok(new JsendSuccess(new { pagination = pagination, vaccines = vaccines }));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpGet("ByPetId/{petId}")]
        public async Task<ActionResult<JsendWrapper>> GetDigitalVaccineByPetId(Guid petId)
        {
            try
            {
                var vaccines = await _context.DigitalVaccines.Where(x => x.PetId == petId).Include("Vaccine").ToListAsync();
                return Ok(new JsendSuccess(vaccines));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JsendWrapper>> GetDigitalVaccineById(Guid id)
        {
            try
            {
                var vaccine = await _context.DigitalVaccines.Where(x => x.Id == id).Include("Vaccine").FirstOrDefaultAsync();
                if (vaccine == null) return NotFound(new JsendFail(new { DigitalVaccine = "NOT_FOUND" }));
                return Ok(new JsendSuccess(vaccine));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JsendWrapper>> PutDigitalVaccine(Guid id, DigitalVaccineDto Dv)
        {
            try
            {
                if (await _context.DigitalVaccines.Where(x => x.Id == id).AnyAsync())
                {
                    var Dv2 = await _context.DigitalVaccines.Where(x => x.Id == id).FirstOrDefaultAsync();
                    Dv2.UserId = Dv.UserId;
                    Dv2.BusinessId = Dv.BusinessId;
                    Dv2.VaccineId = Dv.VaccineId;
                    Dv2.PetId = Dv.PetId;
                    _context.Entry(Dv2).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new JsendSuccess());
                }
                else
                {
                    return NotFound(new JsendFail(new { order = "DigitalVaccine not found" }));
                }
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpGet("Pdf/{id}")]
        public async Task<IActionResult> CreateVaccinePdf(Guid id)
        {
            try
            {
                var vaccine = await _context.DigitalVaccines
                .Where(x => x.Id == id)
                .Include(x => x.Vaccine)
                .Include(x => x.Pet)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync();
                if (vaccine == null)
                {
                    return NotFound(new JsendFail(new { order = "DigitalVaccine not found" }));
                }
                else
                {
                    IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = false;
                    IronPdf.Installation.ChromeGpuMode = IronPdf.Engines.Chrome.ChromeGpuModes.Disabled;
                    IronPdf.Installation.Initialize();
                    var Renderer = new IronPdf.ChromePdfRenderer();
                    var reportTempalte = ReportTemplate();
                    reportTempalte = reportTempalte.Replace("_date", vaccine.Date.ToString());
                    reportTempalte = reportTempalte.Replace("_vaccine", vaccine.Vaccine.VaccineName);
                    reportTempalte = reportTempalte.Replace("_pet", vaccine.Pet.Name);
                    reportTempalte = reportTempalte.Replace("_user", vaccine.Pet.User.Names);
                    var pdf = Renderer.RenderHtmlAsPdf(reportTempalte);
                    return File(pdf.BinaryData, "application/pdf", $"Vaccine {vaccine.Vaccine.VaccineName} - {vaccine.Pet.Name} - {vaccine.Pet.User.Names}.pdf");
                }
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpGet("PdfByteArray2/{id}")]
        public async Task<ActionResult<JsendWrapper>> CreateVaccinePdfAsByteArray(Guid id)
        {
            try
            {
                var vaccine = await _context.DigitalVaccines
                .Where(x => x.Id == id)
                .Include(x => x.Vaccine)
                .Include(x => x.Pet)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync();
                if (vaccine == null)
                {
                    return NotFound(new JsendFail(new { order = "DigitalVaccine not found" }));
                }
                else
                {
                    IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = false;
                    IronPdf.Installation.ChromeGpuMode = IronPdf.Engines.Chrome.ChromeGpuModes.Disabled;
                    IronPdf.Installation.Initialize();
                    var Renderer = new IronPdf.ChromePdfRenderer();
                    var reportTempalte = ReportTemplate();
                    reportTempalte = reportTempalte.Replace("_date", vaccine.Date.ToString());
                    reportTempalte = reportTempalte.Replace("_vaccine", vaccine.Vaccine.VaccineName);
                    reportTempalte = reportTempalte.Replace("_pet", vaccine.Pet.Name);
                    reportTempalte = reportTempalte.Replace("_user", vaccine.Pet.User.Names);
                    var pdf = Renderer.RenderHtmlAsPdf(reportTempalte);
                    return new JsendSuccess(new { response = pdf.BinaryData });
                }
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        private string ReportTemplate()
        {
            string report = "<!DOCTYPE html><html lang='en'><head> <meta charset='UTF-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta name='viewport' content='width=device-width, initial-scale=1.0'> <title>Reporte de Vacuna</title> <style> body { font-family: Calibri, 'Trebuchet MS', sans-serif; font-size: 22px; } .invoice { width: 700px; margin: 0 auto; } .invoice__title { text-align: center; } .invoice__info { display: flex; justify-content: space-between; margin-bottom: 8px; padding: 8px; border-bottom: 1px solid black; } .invoice__info-title { font-weight: bold; } </style></head><body> <div class='invoice'> <h1 class='invoice__title'>Reporte de Vacuna</h1> <div class='invoice__info'> <span class='invoice__info-title'>Vacuna Aplicada</span> <span>_vaccine</span> </div> <div class='invoice__info'> <span class='invoice__info-title'>Fecha de aplicación</span> <span>_date</span> </div> <div class='invoice__info'> <span class='invoice__info-title'>Mascota</span> <span>_pet</span> </div> <div class='invoice__info'> <span class='invoice__info-title'>Cliente</span> <span>_user</span> </div> </div></body></html>";
            return report;
        }
    }
}
