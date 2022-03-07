#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<UserRole>> GetUserRole(Guid id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);

            if (userRole == null)
            {
                return NotFound();
            }

            return userRole;
        }

        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUserRole(UserRoleDto userRoleDto)
        {
            var userRole = userRoleDto.ConvertToType();
            var possibleUser = await _context.UserRoles.Where(x => x.Username == userRole.Username).FirstOrDefaultAsync();
            if (possibleUser == null)
            {
                userRole.Business = await _context.Businesses.Where(x => x.Id == userRole.BusinessId).FirstOrDefaultAsync();
                if (userRole.Business != null)
                {
                    userRole.Password = Methods.ComputeSha256Hash(userRole.Password);
                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetUserRole", new { id = userRole.Id }, new JsendSuccess(new { jwt = GenerateJwtToken(userRole.Username) }));
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
    }
}
