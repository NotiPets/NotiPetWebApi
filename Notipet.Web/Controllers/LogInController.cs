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
        public async Task<ActionResult<UserRole>> LogIn(LoginDto login)
        {
            login.Password = Methods.ComputeSha256Hash(login.Password);
            var userRole = await _context.UserRoles.Where(x => x.Username == login.Username && x.Password == login.Password)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            if (userRole != null)
            {
                userRole.Business = _context.Businesses.Where(x => x.Id == userRole.BusinessId).First();
                return Ok(new JsendSuccess(new
                {
                    jwt = GenerateJwtToken(userRole.Username),
                    userRole = userRole
                }));
            }
            else
            {
                return Unauthorized(new JsendFail(new { credentials = "Invalid credentials" }));
            }
        }
    }
}
