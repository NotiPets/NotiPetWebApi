#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsersController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public UsersController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Users/5
        [HttpGet("{username}")]
        public async Task<ActionResult<JsendWrapper>> GetUser(string username)
        {
            var user = await _context.Users.Where(x => x.Active == true && x.Role != RoleId.Specialist && x.Username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
            user.Password = "Ignore";
            if (user == null)
            {
                return NotFound(new JsendFail(new { credentials = "NOT_FOUND" }));
            }

            return Ok(new JsendSuccess(user));
        }

        // GET: api/Users
        [HttpGet("ByRole/{role}")]
        public async Task<ActionResult<IEnumerable<JsendWrapper>>> GetUsersByRole(RoleId role)
        {
            if (RoleId.Specialist != role)
            {
                var users = await _context.Users.Where(x => x.Active == true && x.Role != RoleId.Specialist).ToListAsync();
                users.ForEach(x => x.Password = "Ignore");
                return Ok(new JsendSuccess(users));
            }
            else
            {
                return BadRequest(new JsendFail(new { role = "This method doesn't handle Specialist" }));
            }
        }

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetUsersByBusiness(Guid businessId)
        {
            var users = await _context.Users.Where(x => x.Active == true && x.Role != RoleId.Specialist && x.BusinessId == businessId).ToListAsync();
            users.ForEach(x => x.Password = "Ignore");
            return Ok(new JsendSuccess(users));
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }*/
    }
}
