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
using Notipet.Web.Validation;
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ValidationControllerBase
    {
        private readonly NotiPetBdContext _context;

        public RatingsController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JsendWrapper>>> GetRatings() => Ok(new JsendSuccess(await _context.Ratings.ToListAsync()));

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> GetRatingByBusinessId(int businessId) => Ok(new JsendSuccess(await _context.Ratings.Where(x => x.BusinessId == businessId).ToListAsync()));

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostRating(RatingDto ratingDto)
        {
            try
            {
                var error = await Validate(ratingDto, new RatingValidation(_context));
                if (error == null)
                {
                    var rating = ratingDto.ConvertToType();
                    _context.Ratings.Add(rating);
                    await _context.SaveChangesAsync();
                    return Ok(new JsendSuccess());
                }
                return error;
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.StackTrace}";
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
