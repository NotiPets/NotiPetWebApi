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
    public class PetsController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public PetsController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            try
            {
                var pets = await _context.Pets.Where(x => x.Active == true && x.User.Active == true).Include("User").ToListAsync();
                foreach (var item in pets)
                {
                    item.User.Password = null;
                    item.PetTypeName = item.PetType.ToString();
                }
                return Ok(new JsendSuccess(pets));
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

        // GET: api/Pets/5
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<Pet>> GetPetsByUserId(Guid userId)
        {
            try
            {
                var pet = await _context.Pets.Where(x => x.UserId == userId && x.Active == true).Include("User").ToListAsync();
                if (pet == null)
                {
                    return NotFound(new JsendFail(new { pet = "NOT_FOUND" }));
                }
                foreach (var item in pet)
                {
                    item.User.Password = null;
                    item.PetTypeName = item.PetType.ToString();
                }
                return Ok(new JsendSuccess(pet));
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

        // GET: api/Pets/5
        [HttpGet("ByPetId/{petId}")]
        public async Task<ActionResult<Pet>> GetPetByPetId(Guid petId)
        {
            try
            {
                var pet = await _context.Pets.Where(x => x.Id == petId).Include("User").FirstOrDefaultAsync();
                if (pet == null)
                {
                    return NotFound(new JsendFail(new { pet = "NOT_FOUND" }));
                }
                pet.User.Password = null;
                pet.PetTypeName = pet.PetType.ToString();
                return Ok(new JsendSuccess(pet));
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

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(Guid id, Pet pet)
        {
            try
            {
                if (id != pet.Id)
                {
                    return BadRequest();
                }

                pet.Created = pet.Created.ToUniversalTime();
                pet.Birthdate = pet.Birthdate.ToUniversalTime();
                var updateDate = (DateTime)pet.Updated;
                pet.Updated = updateDate.ToUniversalTime();

                _context.Entry(pet).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(id))
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

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PetDto>> PostPet(PetDto petdto)
        {
            try
            {
                var pet = petdto.ConvertToType();
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();
                return Ok(new JsendSuccess(pet));
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

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(id);
                if (pet == null)
                {
                    return NotFound();
                }

                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();

                return NoContent();
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

        private bool PetExists(Guid id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}
