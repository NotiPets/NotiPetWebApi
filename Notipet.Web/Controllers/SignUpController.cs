#nullable disable
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
    public class SignUpController : JwtControllerBase
    {
        private readonly NotiPetBdContext _context;

        public SignUpController(IConfiguration config, NotiPetBdContext context) : base(config)
        {
            _context = context;
        }

        [HttpPost("Specialist")]
        public async Task<ActionResult<User>> PostSpecialist(SpecialistDto specialistDto)
        {
            var specialist = specialistDto.ConvertToType();
            if (specialist.User.Role == RoleId.Specialist)
            {
                if (_context.Specialities.Any(x => x.Id == specialist.SpecialityId))
                {
                    var possibleSpecialist = await _context.Specialists.Where(x => x.User.Username == specialist.User.Username).FirstOrDefaultAsync();
                    if (possibleSpecialist == null)
                    {
                        if (DocumentExist(specialist.User.Document))
                        {
                            return Conflict(new JsendFail(new { username = "Document already exist" }));
                        }
                        specialist.User.Business = await _context.Businesses.Where(x => x.Id == specialist.User.BusinessId).FirstOrDefaultAsync();
                        if (specialist.User.Business != null)
                        {
                            specialist.User.Password = Methods.ComputeSha256Hash(specialist.User.Password);
                            _context.Specialists.Add(specialist);
                            await _context.SaveChangesAsync();
                            specialist.User.Password = null;
                            return Ok(new JsendSuccess(new
                            {
                                jwt = GenerateJwtToken(specialist.User.Username),
                                specialist = specialist
                            }));
                        }
                        else
                        {
                            return NotFound(new JsendFail(new { businness = "Business doesn't exist" }));
                        }
                    }
                    else
                    {
                        return Conflict(new JsendFail(new { username = "Username already exist" }));
                    }
                }
                else
                {
                    return BadRequest(new JsendFail(new { specialty = "Specialty doesn't exists" }));
                }
            }
            else
            {
                return BadRequest(new JsendFail(new { role = "The role needs to be Specialist" }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostClient(UserDto userDto)
        {
            try
            {
                var error = await Validate(userDto, new SignUpValidation(_context));
                throw new();
                if (error == null)
                {
                    var user = userDto.ConvertToType();
                    if (user.Role == RoleId.Client)
                    {
                        user.BusinessId = 1;
                    }
                    user.Password = Methods.ComputeSha256Hash(user.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    user.Password = null;
                    return Ok(new JsendSuccess(new
                    {
                        jwt = GenerateJwtToken(user.Username),
                        user = user
                    }));
                }
                return error;
            }
            catch (Exception e)
            {
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, $"{e.Message}\n{e.StackTrace}");
                }
                return StatusCode(500, "¡Oops! Parece que algo pasó...");
            }
        }

        private bool DocumentExist(string doc)
        {
            if (doc == null)
                return false;
            return _context.Users.Any(e => e.Document == doc);
        }
    }
}
