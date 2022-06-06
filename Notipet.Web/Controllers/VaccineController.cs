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
    public class VaccineController : ControllerBase
    {
        private readonly NotiPetBdContext _context;
        public VaccineController(NotiPetBdContext context)
        {
            _context = context;
        }

        [HttpGet("ByBusinessId/{id}")]
        public async Task<ActionResult<JsendWrapper>> GetByBusinessId(int id)
        {
            try
            {
                var vaccines = await _context.Vaccine.Where(x => x.BusinessId == id).ToListAsync();
                if (vaccines == null)
                {
                    return NotFound(new JsendFail(new { DigitalVaccine = "NOT_FOUND" }));
                }
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
        public async Task<ActionResult<JsendWrapper>> GetById(Guid id)
        {
            try
            {
                var vaccine = await _context.Vaccine.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (vaccine == null)
                {
                    return NotFound(new JsendFail(new { DigitalVaccine = "NOT_FOUND" }));
                }
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
    }
}
