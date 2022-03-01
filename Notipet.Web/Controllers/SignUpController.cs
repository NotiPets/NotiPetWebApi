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

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public SignUpController(NotiPetBdContext context)
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


        // POST: api/SignUp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUserRole(UserRole userRole)
        {
            //TODO: log
            var possibleUser = await _context.UserRoles.Where(x => x.Username == userRole.Username).ToListAsync();
            if (!possibleUser.Any())
            {
                string newPassword = String.Empty;
                foreach (var hashByte in SHA256.HashData(Encoding.UTF8.GetBytes(userRole.Password)))
                {
                    newPassword += hashByte.ToString();
                }
                userRole.Password = newPassword;
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                // TODO: mail
                return CreatedAtAction("GetUserRole", new { id = userRole.Id }, new JsendSuccess(userRole));
            }
            else
            {
                return Conflict(new JsendFail(new { username = "Username already exists" }));
            }
        }

        [Route("Dto")]
        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUserRole(UserRoleDto userRoleDto)
        {
            //TODO: log
            var userRole = userRoleDto.ConvertToType();
            var possibleUser = await _context.UserRoles.Where(x => x.Username == userRole.Username).ToListAsync();
            if (!possibleUser.Any())
            {
                string newPassword = String.Empty;
                foreach (var hashByte in SHA256.HashData(Encoding.UTF8.GetBytes(userRole.Password)))
                {
                    newPassword += hashByte.ToString();
                }
                userRole.Password = newPassword;
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                // TODO: mail
                return CreatedAtAction("GetUserRole", new { id = userRole.Id }, new JsendSuccess(userRole));
            }
            else
            {
                return Conflict(new JsendFail(new { username = "Username already exists" }));
            }
        }
    }
}
