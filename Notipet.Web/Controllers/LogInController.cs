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

namespace Notipet.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogInController : Controller
    {
        private readonly NotiPetBdContext _context;
        public IConfiguration _configuration;

        public LogInController(IConfiguration config, NotiPetBdContext context)
        {
            _context = context;
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Login login)
        {
            //verifying nulls
            if (login == null || login.username == null || login.password == null)
            {
                var responsemodel = (new ResponseModel
                {
                    status = "fail",
                    data = null,
                    message = "DOESNT_EXIST"
                });
                return NotFound(responsemodel);
            }

            UserRole search1 = new UserRole();
            UserRole search2 = new UserRole();

            //Search itself
            try
            {
                //Pass to hash
                login.password = Login.ComputeSha256Hash(login.password);

                search1 = await _context.UserRoles
                    .FirstOrDefaultAsync(m => m.Username == login.username);

                search2 = await _context.UserRoles
                    .FirstOrDefaultAsync(m => m.Username == login.username && m.Password == login.password);
            }
            catch (Exception)
            {
                var responsemodel = (new ResponseModel
                {
                    status = "error",
                    data = null,
                    message = "INTERNAL_ERROR"
                });
                return Problem(responsemodel.ToString());
            }

            //Login user not found

            if (search1 == null)
            {
                var responsemodel = (new ResponseModel
                {
                    status = "fail",
                    data = null,
                    message = "DOESNT_EXIST"
                });
                return NotFound(responsemodel);
            }

            //Login invalid credentials
            if (search2 == null)
            {
                var responsemodel = (new ResponseModel
                {
                    status = "fail",
                    data = null,
                    message = "INVALID_CREDENTIALS"
                });
                return NotFound(responsemodel);
            }


            //Token generator
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("username", search2.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            //Response 
            var loginresponse = (new LoginResponse
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                registered = true,
                email = search2.Email,
                username = search2.Username
            });
            var SuccessResponse = (new ResponseModel
            {
                status = "success",
                data = loginresponse,
                message = "AUTHENTICATED"
            });
            return Ok(SuccessResponse);

        }
        //Models
        public class example
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }

            public static string ComputeSha256Hash(string rawData)
            {
                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

        }
        public class ResponseModel
        {
            public string status { get; set; }
            public object data { get; set; }
            public string message { get; set; }
        }
        public class LoginResponse
        {
            public string token { get; set; }
            public bool registered { get; set; }
            public string email { get; set; }
            public string username { set; get; }
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
