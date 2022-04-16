#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class SpecialistsController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public SpecialistsController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Specialists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialists()
        {
            try
            {
                var specialists = await _context.Specialists.Where(x => x.User.Active == true).Include("User").ToListAsync();
                for (int i = 0; i < specialists.Count; i++)
                {
                    specialists[i].User.Password = null;
                    specialists[i].Speciality = await _context.Specialities.Where(x => x.Id == specialists[i].SpecialityId).FirstOrDefaultAsync();
                }
                return Ok(new JsendSuccess(specialists));
            }
            catch (Exception e)
            {
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError($"{e.Message}\n{e.StackTrace}"));
                }
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        // GET: api/Specialists/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Specialist>> GetSpecialist(string username)
        {
            try
            {
                var specialist = await _context.Specialists.Where(x => x.User.Active == true && x.User.Username.ToLower() == username.ToLower())
                                .Include("User")
                                .FirstOrDefaultAsync();
                if (specialist != null)
                {
                    specialist.User.Password = null;
                    specialist.Speciality = await _context.Specialities.Where(x => x.Id == specialist.SpecialityId).FirstOrDefaultAsync();
                    return Ok(new JsendSuccess(specialist));
                }
                return NotFound(new JsendFail(new { notFounf = "Specialist not found" }));
            }
            catch (Exception e)
            {
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError($"{e.Message}\n{e.StackTrace}"));
                }
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }

        // PUT: api/Specialists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialist(Guid id, SpecialistDto specialistDto)
        {
            var specialist = specialistDto.ConvertToType();
            var specialists = await _context.Specialists.Where(x => x.User.Active == true && x.SpecialityId == specialist.SpecialityId).Include("User").ToListAsync();
            for (int i = 0; i < specialists.Count; i++)
            {
                specialists[i].Speciality = await _context.Specialities.Where(x => x.Id == specialists[i].SpecialityId).FirstOrDefaultAsync();
            }
            return Ok(new JsendSuccess(await GetSpecialistsDtoAsync(specialists)));
        }*/

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialistsByBusiness(int businessId)
        {
            try
            {
                var specialists = await _context.Specialists.Where(x => x.User.Active == true && x.User.BusinessId == businessId).Include("User").ToListAsync();
                for (int i = 0; i < specialists.Count; i++)
                {
                    specialists[i].Speciality = await _context.Specialities.Where(x => x.Id == specialists[i].SpecialityId).FirstOrDefaultAsync();
                }
                return Ok(new JsendSuccess(specialists));
            }
            catch (Exception e)
            {
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError($"{e.Message}\n{e.StackTrace}"));
                }
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }

        [HttpPut("specialist")]
        public async Task<IActionResult> PutSpecialist(Guid specialistId, SpecialistDto specialistDto)
        {
            try
            {
                // This needs to reject the password in the User object (prolly new DTO)
                // We really don't need this much information
                var specialist = specialistDto.ConvertToType();
                specialist.Id = specialistId;
                specialist.User.Id = _context.Users.Where(x => x.Username == specialist.User.Username).Select(x => x.Id).FirstOrDefault();
                _context.Entry(specialist.User).State = EntityState.Modified;
                _context.Entry(specialist).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialistExists(specialist.Id))
                    {
                        return NotFound(new JsendFail(new { notFound = "Specialist not found" }));
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(new JsendSuccess(new { }));
            }
            catch (Exception e)
            {
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError($"{e.Message}\n{e.StackTrace}"));
                }
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteSpecialist(Guid specialistId)
        {
            try
            {
                var specialist = await _context.Specialists.Where(x => x.Id == specialistId && x.User.Active == true).Include("User").FirstOrDefaultAsync();
                if (specialist != null)
                {
                    specialist.User.Active = false;
                    _context.Entry(specialist.User).State = EntityState.Modified;
                    _context.Entry(specialist).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new JsendSuccess(new { }));
                }
                return NotFound(new JsendFail(new { notFound = "Specialist not found" }));
            }
            catch (Exception e)
            {
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError($"{e.Message}\n{e.StackTrace}"));
                }
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }

        private bool SpecialistExists(Guid id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }
    }
}
