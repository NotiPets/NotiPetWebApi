﻿using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Domain;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;
using Notipet.Web.Services;
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalVaccineController : ControllerBase
    {
        private readonly NotiPetBdContext _context;
        private readonly IReportService _reportService;
        public DigitalVaccineController(NotiPetBdContext context, IConverter converter)
        {
            _context = context;
            _reportService = new ReportService(converter);
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
                    return NotFound(new JsendFail(new { order = "order not found" }));
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
            var pdfFile = _reportService.GeneratePdfReport();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }
    }
}
