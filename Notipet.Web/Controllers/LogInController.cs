#nullable disable
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            UserRole search = new UserRole();

            search = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.Username == login.Username && m.Password == login.Password); //Para verificar que user y password matchean

            if (search == null)
            {
                return Unauthorized(new JsendFail(new { credentials = "INVALID_CREDENTIALS" }));
            }
            else
            {
                var data = (new LoginResponseDto
                {
                    Token = GenerateJwtToken(search.Username),
                    Username = search.Username,
                    Email = search.Email,
                    BusinessId = search.BusinessId.ToString()
                });
                return Ok(new JsendSuccess(data));
            }
        }
    }
}
