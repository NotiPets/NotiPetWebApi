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
    public class EmployeesController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public EmployeesController(NotiPetBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetEmployees()
        {
            List<User> users = new List<User>();
            users = await _context.Users.Where(x => x.Role == RoleId.Seller && x.Active == true).ToListAsync();
            return Ok(new JsendSuccess(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetEmployee(Guid id)
        {
            var employee = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id && m.Role == RoleId.Seller);

            if (employee == null)
            {
                return NotFound(new JsendFail(new { credentials = "EMPLOYEE_NOT_FOUND" }));
            }
            return Ok(new JsendSuccess(employee));
        }

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<UserDto>> GetEmployeeByBusinessId(int businessId)
        {
            var employee = await _context.Users
                .FirstOrDefaultAsync(m => m.BusinessId == businessId && m.Role == RoleId.Seller);

            if (employee == null)
            {
                return NotFound(new JsendFail(new { credentials = "EMPLOYEE_NOT_FOUND" }));
            }
            return Ok(new JsendSuccess(employee));
        }

        [HttpPut]
        public async Task<IActionResult> PutEmployee(UserDto userDto, Guid userId)
        {
            var user = userDto.CovertToType();
            user.Id = userId;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound(new JsendFail(new { notFound = "USER_NOT_FOUND" }));
                }
                else
                {
                    throw;
                }
            }
            return Ok(new JsendSuccess(new { }));
        }


        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id && e.Role == RoleId.Seller);
        }
    }
}
