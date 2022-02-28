#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notipet.Data;
using Notipet.Domain;

namespace Notipet.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRolesController : Controller
    {
        private readonly NotiPetBdContext _context;
        public IConfiguration _configuration;

        public UserRolesController(IConfiguration config, NotiPetBdContext context)
        {
            _context = context;
            _configuration = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? id, string? password)
        {
            //if (id == null || password == null)
            //     return NotFound();

            // var userRole = await _context.UserRoles
            //     .FirstOrDefaultAsync(m => m.Id == id && m.Password == password);
            // if (userRole == null)
            // {
            //     return NotFound();
            // }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));


        }

        //// GET: UserRoles
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.UserRoles.ToListAsync());
        //}

        //// GET: UserRoles/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userRole = await _context.UserRoles
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (userRole == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userRole);
        //}

        //// GET: UserRoles/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserRoles/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Role,Active,Username,Password,Email,Created,Updated")] UserRole userRole)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        userRole.Id = Guid.NewGuid();
        //        _context.Add(userRole);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userRole);
        //}

        //// GET: UserRoles/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userRole = await _context.UserRoles.FindAsync(id);
        //    if (userRole == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userRole);
        //}

        //POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,Role,Active,Username,Password,Email,Created,Updated")] UserRole userRole)
        //{
        //    if (id != userRole.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userRole);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserRoleExists(userRole.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userRole);
        //}

        //// GET: UserRoles/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userRole = await _context.UserRoles
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (userRole == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userRole);
        //}

        //POST: UserRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var userRole = await _context.UserRoles.FindAsync(id);
        //    _context.UserRoles.Remove(userRole);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserRoleExists(Guid id)
        //{
        //    return _context.UserRoles.Any(e => e.Id == id);
        //}
    }
}
