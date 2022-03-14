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
            var user = userDto.CovertToType();
            var possibleUser = await _context.Users.Where(x => x.Username == user.Username).FirstOrDefaultAsync();
            if (possibleUser == null)
            {
                user.Business = await _context.Businesses.Where(x => x.Id == user.BusinessId).FirstOrDefaultAsync();
                if (user.Business != null)
                {
                    user.Password = Methods.ComputeSha256Hash(user.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetUserRole", new { id = user.Id }, new JsendSuccess(new { jwt = GenerateJwtToken(user.Username) }));
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
