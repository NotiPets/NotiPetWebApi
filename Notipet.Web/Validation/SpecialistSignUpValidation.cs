using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;

namespace Notipet.Web.Validation
{
    public class SpecialistSignUpValidation : ValidationBase
    {

        public SpecialistSignUpValidation(NotiPetBdContext context) : base(context)
        {

        }

        public async Task<ActionResult<JsendWrapper>> IsSpecialist(SpecialistDto specialistDto)
        {
            if (specialistDto.User.Role == Domain.RoleId.Specialist)
            {
                return null;
            }
            return BadRequest(new JsendFail(new { role = "The role needs to be Specialist" }));
        }

        public async Task<ActionResult<JsendWrapper>?> UsernameDoesNotExist(SpecialistDto specialistDto)
        {
            if (!await _context.Specialists.Where(x => x.User.Username == specialistDto.User.Username).AnyAsync())
            {
                return null;
            }
            return Conflict(new JsendFail(new { username = "Username already exist" }));
        }

        public async Task<ActionResult<JsendWrapper>?> DocumentDoesNotExist(SpecialistDto specialistDto)
        {
            if (!await _context.Users.Where(x => x.Document == specialistDto.User.Document).AnyAsync())
            {
                return null;
            }
            return Conflict(new JsendFail(new { username = "Document already exist" }));
        }

        public async Task<ActionResult<JsendWrapper>?> BusinessDoesExist(SpecialistDto specialistDto)
        {
            if (await _context.Businesses.Where(x => x.Id == specialistDto.User.BusinessId).AnyAsync())
            {
                return null;
            }
            return NotFound(new JsendFail(new { businness = "Business doesn't exist" }));
        }

        public async Task<ActionResult<JsendWrapper>?> SpecialtyExist(SpecialistDto specialistDto)
        {
            if (await _context.Specialities.Where(x => x.Id == specialistDto.SpecialtyId).AnyAsync())
            {
                return null;
            }
            return BadRequest(new JsendFail(new { specialty = "Specialty doesn't exists" }));
        }
    }
}
