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

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitiesController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public SpecialitiesController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Specialities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpecialities()
        {
            var data = await _context.Specialities.ToListAsync();
            return Ok(new JsendSuccess(data));
        }

        // GET: api/Specialities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Speciality>> GetSpeciality(int id)
        {
            var speciality = await _context.Specialities.FindAsync(id);

            if (speciality == null)
            {
                return NotFound(new JsendFail(new { Specialty = "NOT_FOUND" }));
            }

            return Ok(new JsendSuccess(speciality));
        }

        // PUT: api/Specialities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeciality(int id, Speciality speciality)
        {
            if (id != speciality.Id)
            {
                return BadRequest();
            }

            _context.Entry(speciality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Specialities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Speciality>> PostSpeciality(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
            await _context.SaveChangesAsync();
            var a = await _context.Specialities.FindAsync(speciality.Id);
            return Ok(new JsendSuccess(a));
        }

        private bool SpecialityExists(int id)
        {
            return _context.Specialities.Any(e => e.Id == id);
        }
    }
}
