using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;

namespace Notipet.Web.Validation
{
    public class UserSignUpValidation : ValidationBase
    {

        public UserSignUpValidation(NotiPetBdContext context) : base(context)
        {

        }
        public async Task<ActionResult<JsendWrapper>?> UsernameDoesNotExist(UserDto userDto)
        {
            if (!await _context.Users.Where(x => x.Username == userDto.Username).AnyAsync())
            {
                return null;
            }
            return Conflict(new JsendFail(new { username = "USERNAME_ALREADY_EXISTS" }));
        }

        public async Task<ActionResult<JsendWrapper>?> DocumentDoesNotExist(UserDto userDto)
        {
            if (!await _context.Users.Where(x => x.Document == userDto.Document).AnyAsync())
            {
                return null;
            }
            return Conflict(new JsendFail(new { username = "DOCUMENT_ALREADY_EXISTS" }));
        }

        public async Task<ActionResult<JsendWrapper>?> BusinessDoesExist(UserDto userDto)
        {
            if (await _context.Businesses.Where(x => x.Id == userDto.BusinessId).AnyAsync())
            {
                return null;
            }
            return NotFound(new JsendFail(new { businness = "BUSINESS_DOESN'T_EXISTS" }));
        }

        public async Task<ActionResult<JsendWrapper>> IsNotSpecialist(SpecialistDto specialistDto)
        {
            if (specialistDto.User.Role != Domain.RoleId.Specialist)
            {
                return null;
            }
            return BadRequest(new JsendFail(new { role = "The role needs to be Specialist" }));
        }
    }
}
