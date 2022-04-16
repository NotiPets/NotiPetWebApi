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
    public class BusinessesController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public BusinessesController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Businesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinesses() => Ok(new JsendSuccess(await _context.Businesses.ToListAsync()));
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinessesById(int id) => Ok(new JsendSuccess(await _context.Businesses.Where(x => x.Id == id).FirstOrDefaultAsync()));

        // PUT: api/Businesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusiness(int id, Business business)
        {
            try
            {
                if (id != business.Id)
                {
                    return BadRequest();
                }

                _context.Entry(business).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessExists(id))
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
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError($"{e.Message}\n{e.StackTrace}"));
                }
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        // POST: api/Businesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Business>> PostBusiness(BusinessDto businessDto)
        {
            try
            {
                var business = businessDto.ConvertToType();
                _context.Businesses.Add(business);
                await _context.SaveChangesAsync();
                return Ok(new JsendSuccess(business));
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

        private bool BusinessExists(int id)
        {
            return _context.Businesses.Any(e => e.Id == id);
        }
    }
}
