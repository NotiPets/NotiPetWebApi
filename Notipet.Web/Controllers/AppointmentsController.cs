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
using Notipet.Web.Validation;
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ValidationControllerBase
    {
        private readonly NotiPetBdContext _context;

        public AppointmentsController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JsendWrapper>> GetAppointment(Guid id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment != null)
                {
                    appointment.Specialist = await _context.Specialists.Include("User").Where(x => x.Id == appointment.SpecialistId).FirstOrDefaultAsync();
                    appointment.Specialist.Speciality = await _context.Specialities.FindAsync(appointment.Specialist.SpecialityId);
                }
                return Ok(new JsendSuccess(appointment));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        // GET: api/Appointments/5
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<JsendWrapper>> GetAppointmentsByUser(Guid userId)
        {
            try
            {
                var appointments = await _context.Orders
                    .Where(x => x.AssetsServices.AssetsServiceType == AssetsServiceTypeId.Service && x.UserId == userId)
                    .Include(x => x.Appointment.Specialist)
                    .ThenInclude(x => x.Speciality)
                    .Select(x => x.Appointment)
                    .ToListAsync();
                return Ok(new JsendSuccess(appointments));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        // GET: api/Appointments/5
        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> GetAppointmentsByBusiness(int businessId)
        {
            try
            {
                var appointments = await _context.Sales
                    .Where(x => x.BusinessId == businessId && x.Orders.Where(x => x.AssetsServices.AssetsServiceType == AssetsServiceTypeId.Service).Any())
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.Appointment)
                    .ThenInclude(x => x.Specialist)
                    .ThenInclude(x => x.Speciality)
                    .Select(x => x.Orders.Select(x => x.Appointment))
                    .ToListAsync();
                return Ok(new JsendSuccess(appointments));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JsendWrapper>> PutAppointment(Guid id, AppointmentDto appointmentDto)
        {
            try
            {
                var appointment = appointmentDto.ConvertToType();
                appointment.Id = id;
                _context.Entry(appointment).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(id))
                    {
                        return BadRequest(new JsendFail(new { appointment = "Appointment not found" }));
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(new JsendSuccess());
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }
        }

        private bool AppointmentExists(Guid id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
