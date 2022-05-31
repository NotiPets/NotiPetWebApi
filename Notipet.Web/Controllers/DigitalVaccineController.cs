﻿using Microsoft.AspNetCore.Http;
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
        public DigitalVaccineController(NotiPetBdContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostDigitalVaccione(DigitalVaccineDto digitalVaccine)
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
        public async Task<ActionResult<IEnumerable<DigitalVaccine>>> GetDigitalVaccineByBusinessId(int businessId)
        {
            try
            {
                var vaccines = await _context.DigitalVaccines.Where(x => x.BusinessId == businessId).Include("Vaccine").ToListAsync();
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
        [HttpGet("ByPetId/{petId}")]
        public async Task<ActionResult<IEnumerable<DigitalVaccine>>> GetDigitalVaccineByPetId(Guid petId)
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
        public async Task<ActionResult<IEnumerable<DigitalVaccine>>> GetDigitalVaccineById(Guid id)
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
    }
}
