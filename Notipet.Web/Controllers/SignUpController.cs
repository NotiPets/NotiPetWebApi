#nullable disable
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
    public class SignUpController : JwtBaseController
    {
        private readonly NotiPetBdContext _context;

        public SignUpController(IConfiguration config, NotiPetBdContext context) : base(config)
        {
            _context = context;
        }

        // GET: api/SignUp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserRole(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUserRole(UserDto userDto)
        {
            userDto.Role = RoleId.Client;
            return await PostUser(userDto); 
        }

        [Route("employee")]
        [HttpPost]
        public async Task<ActionResult<User>> PostEmployee(UserDto userDto)
        {
            userDto.Role = RoleId.Seller;
            return await PostUser(userDto);
        }

        private bool DocumentExist(string doc)
        {
            if (doc == null)
                return false;
            return _context.Users.Any(e => e.Document == doc);
        }

        private async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var user = userDto.CovertToType();
            var possibleUser = await _context.Users.Where(x => x.Username == user.Username).FirstOrDefaultAsync();
            if (possibleUser == null)
            {
                if (DocumentExist(user.Document))
                {
                    return Conflict(new JsendFail(new { username = "DOCUMENT_ALREADY_EXISTS" }));
                }
                user.Business = await _context.Businesses.Where(x => x.Id == user.BusinessId).FirstOrDefaultAsync();
                if (user.Business != null)
                {
                    user.Password = Methods.ComputeSha256Hash(user.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    var data = (new LoginResponseDto
                    {
                        Token = GenerateJwtToken(user.Username),
                        Username = user.Username,
                        Email = user.Email,
                        BusinessId = user.BusinessId.ToString(),
                        UserId = user.Id.ToString()
                    });
                    return Ok(new JsendSuccess(data));
                }
                else
                {
                    return NotFound(new JsendFail(new { businness = "BUSINESS_DOESN'T_EXISTS" }));
                }
            }
            else
            {
                return Conflict(new JsendFail(new { username = "USERNAME_ALREADY_EXISTS" }));
            }
        }
    }
}
