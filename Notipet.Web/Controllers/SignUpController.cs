﻿#nullable disable
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

        [HttpPost("Specialist")]
        public async Task<ActionResult<User>> PostSpecialist(SpecialistSignUpDto specialistDto)
        {
            var specialist = specialistDto.CovertToType();
            if (specialist.User.Role == RoleId.Specialist)
            {
                if (_context.Specialities.Any(x => x.Id == specialist.SpecialityId))
                {
                    var possibleSpecialist = await _context.Specialists.Where(x => x.User.Username == specialist.User.Username).FirstOrDefaultAsync();
                    if (possibleSpecialist == null)
                    {
                        if (DocumentExist(specialist.User.Document))
                        {
                            return Conflict(new JsendFail(new { username = "Document already exist" }));
                        }
                        specialist.User.Business = await _context.Businesses.Where(x => x.Id == specialist.User.BusinessId).FirstOrDefaultAsync();
                        if (specialist.User.Business != null)
                        {
                            specialist.User.Password = Methods.ComputeSha256Hash(specialist.User.Password);
                            _context.Specialists.Add(specialist);
                            await _context.SaveChangesAsync();
                            var data = (new LoginResponseDto
                            {
                                Token = GenerateJwtToken(specialist.User.Username),
                                Username = specialist.User.Username,
                                Email = specialist.User.Email,
                                BusinessId = specialist.User.BusinessId.ToString(),
                                // Prolly needs to return SpecialistId here
                                UserId = specialist.User.Id.ToString()
                            });
                            return Ok(new JsendSuccess(data));
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
                else
                {
                    return BadRequest(new JsendFail(new { specialty = "Specialty doesn't exists" }));
                }
            }
            else
            {
                return BadRequest(new JsendFail(new { role = "The role needs to be Specialist" }));
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostClient(UserDto userDto)
        {
            userDto.Role = RoleId.Client;
            return await PostUser(userDto);
        }

        [Route("employee")]
        [HttpPost]
        public async Task<ActionResult<User>> PostEmployee(UserDto userDto)
        {
            userDto.Role = RoleId.Seller;
            return await PostUser(userDto);
        }

        private bool DocumentExist(string doc)
        {
            if (doc == null)
                return false;
            return _context.Users.Any(e => e.Document == doc);
        }

        private async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var user = userDto.CovertToType();
            var possibleUser = await _context.Users.Where(x => x.Username == user.Username).FirstOrDefaultAsync();
            if (possibleUser == null)
            {
                if (DocumentExist(user.Document))
                {
                    return Conflict(new JsendFail(new { username = "DOCUMENT_ALREADY_EXISTS" }));
                }
                user.Business = await _context.Businesses.Where(x => x.Id == user.BusinessId).FirstOrDefaultAsync();
                if (user.Business != null)
                {
                    user.Password = Methods.ComputeSha256Hash(user.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    var data = (new LoginResponseDto
                    {
                        Token = GenerateJwtToken(user.Username),
                        Username = user.Username,
                        Email = user.Email,
                        BusinessId = user.BusinessId.ToString(),
                        UserId = user.Id.ToString()
                    });
                    return Ok(new JsendSuccess(data));
                }
                else
                {
                    return NotFound(new JsendFail(new { businness = "BUSINESS_DOESN'T_EXISTS" }));
                }
            }
            else
            {
                return Conflict(new JsendFail(new { username = "USERNAME_ALREADY_EXISTS" }));
            }
        }
    }
}
