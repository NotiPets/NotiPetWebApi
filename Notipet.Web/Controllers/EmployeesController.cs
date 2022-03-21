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

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetEmployees()
        {
            List<User> users = new List<User>();
            users = await _context.Users.Where(x => x.Role == RoleId.Seller).ToListAsync();
            foreach (var item in users)
            {
                item.Password = "";
            }
            return Ok(new JsendSuccess(users));
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetEmployee(Guid id)
        {
            var employee = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id && m.Role == RoleId.Seller);

            if (employee == null)
            {
                return NotFound(new JsendFail(new { credentials = "EMPLOYEE_NOT_FOUND" }));
            }

            employee.Password = "";
            return Ok(new JsendSuccess(employee));
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, User user)
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
        }
    }
}
