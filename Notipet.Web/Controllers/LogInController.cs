#nullable disable
using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Domain;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;
using Utilities;

namespace Notipet.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogInController : JwtBaseController
    {
        private readonly NotiPetBdContext _context;

        public LogInController(IConfiguration config, NotiPetBdContext context) : base(config)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<LoginDto>> LogIn(LoginDto login)
        {
            try
            {
                login.Password = Methods.ComputeSha256Hash(login.Password);
                User search = new User();

                search = await _context.Users
                    .FirstOrDefaultAsync(m => m.Username == login.Username && m.Password == login.Password); //Para verificar que user y password matchean

                if (search != null)
                {
                    search.Password = null;
                    return Ok(new JsendSuccess(new
                    {
                        jwt = GenerateJwtToken(search.Username),
                        user = search,

                    }));
                }
                else
                {
                    return Unauthorized(new JsendFail(new { credentials = "INVALID_CREDENTIALS" }));
                }
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
    }
}
