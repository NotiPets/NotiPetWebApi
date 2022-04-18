using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;

namespace Notipet.Web.Validation
{
    public class RatingValidation : ValidationBase
    {
        public RatingValidation(NotiPetBdContext context) : base(context) { }
        public async Task<ActionResult<JsendWrapper>?> UserDoesExist(RatingDto ratingDto)
        {
            if (await _context.Users.Where(x => x.Id == ratingDto.UserId).AnyAsync())
            {
                return null;
            }
            return NotFound(new JsendFail(new { user = "user doesn't exists" }));
        }

        public async Task<ActionResult<JsendWrapper>?> BusinessDoesExist(RatingDto ratingDto)
        {
            if (await _context.Businesses.Where(x => x.Id == ratingDto.BusinessId).AnyAsync())
            {
                return null;
            }
            return NotFound(new JsendFail(new { businness = "business doesn't exists" }));
        }

        public async Task<ActionResult<JsendWrapper>?> RatingBetween1and5(RatingDto ratingDto)
        {
            if (ratingDto.RatingNumber >= 1 && ratingDto.RatingNumber <= 5)
            {
                return null;
            }
            return BadRequest(new JsendFail(new { rating = "rating must be between 1 and 5" }));
        }
    }
}
