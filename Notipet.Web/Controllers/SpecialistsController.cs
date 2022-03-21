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
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialists() => Ok(new JsendSuccess(await GetSpecialistDto(await _context.Specialists.Include("Speciality").Include("User").ToListAsync())));

        // GET: api/Specialists/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Specialist>> GetSpecialist(string username)
        {
            var specialist = await _context.Specialists.Where(x => x.User.Active == true && x.User.Username.ToLower() == username.ToLower())
                .Include("Speciality")
                .Include("User")
                .FirstOrDefaultAsync();

            if (specialist == null)
            {
                return NotFound(new JsendFail(new { credentials = "NOT_FOUND" }));
            }

            var specialistDto = new SpecialistDto
            {
                Name = specialist.User.Names.ToString(),
                LastName = specialist.User.Lastnames.ToString(),
                Specialty = specialist.Speciality.Name.ToString(),
                Rating = 1,
                BusinessId = specialist.User.BusinessId.ToString(),
                Description = specialist.Speciality.Description.ToString(),
                PictureUrl = specialist.User.PictureUrl.ToString(),
            };

            return Ok(new JsendSuccess(specialistDto));
        }

        [HttpGet("BySpeciality/{specialityId}")]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialistBySpeciality(int specialityId) => Ok(new JsendSuccess(await GetSpecialistDto(await _context.Specialists.Where(x => x.Speciality.Id == specialityId).Include("Speciality").Include("User").ToListAsync())));

        private async Task<List<SpecialistDto>> GetSpecialistDto(List<Specialist> specialist)
        {
            SpecialistDto specialistDto = new SpecialistDto();
            List<SpecialistDto> specialistsMapped = new List<SpecialistDto>();
            foreach (var item in specialist)
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
            return specialistsMapped;
        }

        // PUT: api/Specialists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
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

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }*/
    }
}
