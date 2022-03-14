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
            {
                SpecialistDto specialistDto = new SpecialistDto();
                List<Specialist> specialists = await _context.Specialists.ToListAsync();
                return Ok(specialists);
                List<SpecialistDto> specialistsMapped = new List<SpecialistDto>();
                foreach (var item in specialists)
                {
                    specialistDto.Name = item.User.Names.ToString();
                    specialistDto.LastName = item.User.Lastnames.ToString();
                    specialistDto.Specialty = item.Speciality.Name.ToString();
                    specialistDto.Rating = 1;
                    specialistDto.BusinessId = item.User.BusinessId.ToString();
                    specialistDto.Description = item.Speciality.Description.ToString();
                    specialistDto.PictureUrl = item.User.PictureUrl.ToString();
                    specialistsMapped.Add(specialistDto);
                }
                return Ok(new JsendSuccess(specialistDto));
            }
        }

        // GET: api/Specialists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialist>> GetSpecialist(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);

            if (specialist == null)
            {
                return NotFound();
            }

            return specialist;
        }

        // PUT: api/Specialists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialist(int id, Specialist specialist)
        {
            if (id != specialist.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialistExists(id))
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

        // POST: api/Specialists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialist>> PostSpecialist(Specialist specialist)
        {
            _context.Specialists.Add(specialist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialist", new { id = specialist.Id }, specialist);
        }

        // DELETE: api/Specialists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialist(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }

            _context.Specialists.Remove(specialist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }
    }
}
