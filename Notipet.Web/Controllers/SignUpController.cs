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
        public async Task<ActionResult<JsendWrapper>> PostSpecialist(SpecialistDto specialistDto)
        {
            try
            {
                var error = await Validate(specialistDto, new SpecialistSignUpValidation(_context));
                if (error == null)
                {
                    var specialist = specialistDto.ConvertToType();
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

        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostClient(UserDto userDto)
        {
            try
            {
                var error = await Validate(userDto, new UserSignUpValidation(_context));
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
