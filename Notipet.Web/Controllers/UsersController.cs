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
using Utilities;

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
            try
            {
                var user = await _context.Users.Where(x => x.Active == true && x.Role != RoleId.Specialist && x.Username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound(new JsendFail(new { credentials = "NOT_FOUND" }));
                }
                user.Password = "Ignore";

                return Ok(new JsendSuccess(user));
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

        // GET: api/Users
        [HttpGet("ByRole/{role}")]
        public async Task<ActionResult<IEnumerable<JsendWrapper>>> GetUsersByRole(RoleId role)
        {
            try
            {
                if (RoleId.Specialist != role)
                {
                    var users = await _context.Users.Where(x => x.Active == true && x.Role == role).ToListAsync();
                    if (users == null)
                        return Ok(new JsendSuccess(users));
                    users.ForEach(x => x.Password = "Ignore");
                    return Ok(new JsendSuccess(users));
                }
                else
                {
                    return BadRequest(new JsendFail(new { role = "This method doesn't handle Specialist" }));
                }
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

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetUsersByBusiness(int businessId)
        {
            try
            {
                var users = await _context.Users.Where(x => x.Active == true && x.Role != RoleId.Specialist && x.BusinessId == businessId).ToListAsync();
                users.ForEach(x => x.Password = "Ignore");
                return Ok(new JsendSuccess(users));
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

        [HttpPut("userId")]
        public async Task<IActionResult> PutUser(Guid userId, UserDtoPut userDto)
        {
            try
            {
                // This needs to reject the password in the object (prolly new DTO)
                var user1 = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
                if (!UserExists(userId))
                {
                    return NotFound(new JsendFail(new { notFound = "User not found" }));
                }
                var user = userDto.ConvertToType(user1.Password.ToString());
                user.Id = userId;
                _context.Users.Update(user);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Ok(new JsendSuccess(new { }));
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

        [HttpDelete()]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                var user = await _context.Users.Where(x => x.Active == true && x.Id == userId).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Active = false;
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new JsendSuccess(new { }));
                }
                return NotFound(new JsendFail(new { notFound = "User not found" }));
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

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
