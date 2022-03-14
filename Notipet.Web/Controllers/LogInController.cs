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
        public async Task<ActionResult<LoginResponseDto>> LogIn(LoginDto login)
        {
            login.Password = Methods.ComputeSha256Hash(login.Password);
            User search = new User();

            search = await _context.Users
                .FirstOrDefaultAsync(m => m.Username == login.Username && m.Password == login.Password); //Para verificar que user y password matchean

            if (search != null)
            {
                var data = (new LoginResponseDto
                {
                    Token = GenerateJwtToken(search.Username),
                    Username = search.Username,
                    Email = search.Email,
                    BusinessId = search.BusinessId.ToString(),
                    UserId = search.Id.ToString()
                });
                return Ok(new JsendSuccess(data));
            }
            else
            {
                return Unauthorized(new JsendFail(new { credentials = "INVALID_CREDENTIALS" }));
            }
        }
    }
}
