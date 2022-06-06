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
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public DashboardController(NotiPetBdContext context)
        {
            _context = context;
        }

        [HttpGet("AppointmentCompleted/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> AppointmentCompleted(int businessId)
        {
            try
            {
                var appointments = await _context.Appointments
                    .Where(appointment => appointment.AppointmentStatus == AppointmentStatusId.Completed &&
                        _context.Orders.Where(order => appointment.Id == order.Appointment.Id &&
                            _context.Sales.Where(sale => sale.Orders.Contains(order) && sale.BusinessId == businessId).Any()).Any()).ToListAsync();
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

        [HttpGet("AppointmentPending/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> AppointmentPending(int businessId)
        {
            try
            {
                var appointments = await _context.Appointments
                    .Where(appointment => appointment.AppointmentStatus == AppointmentStatusId.Requested &&
                        _context.Orders.Where(order => appointment.Id == order.Appointment.Id &&
                            _context.Sales.Where(sale => sale.Orders.Contains(order) && sale.BusinessId == businessId).Any()).Any()).ToListAsync();
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

        [HttpGet("AppliedVaccines/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> AppliedVaccines(int businessId) => Ok(new JsendSuccess(await _context.DigitalVaccines.Where(x => x.BusinessId == businessId).ToListAsync()));

        [HttpGet("Last7DaysAppointments/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> Last7DaysAppointments(int businessId)
        {
            try
            {
                var appointments = await _context.Appointments
                    .Where(appointment => appointment.Created >= DateTime.UtcNow.AddDays(-7) &&
                        _context.Orders.Where(order => appointment.Id == order.Appointment.Id &&
                            _context.Sales.Where(sale => sale.Orders.Contains(order) && sale.BusinessId == businessId).Any()).Any()).ToListAsync();
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

        [HttpGet("DetailedLast7DaysAppointments/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> DetailedLast7DaysAppointments(int businessId)
        {
            try
            {
                List<int> detail = new List<int>();
                for (int i = 0; i >= -7; i--)
                {
                    var appointments = await _context.Appointments
                        .Where(appointment => appointment.Created.Date == DateTime.UtcNow.AddDays(i).Date &&
                            _context.Orders.Where(order => appointment.Id == order.Appointment.Id &&
                                _context.Sales.Where(sale => sale.Orders.Contains(order) && sale.BusinessId == businessId).Any()).Any()).ToListAsync();
                    detail.Add(appointments != null ? appointments.Count : 0);
                }
                return Ok(new JsendSuccess(detail));
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
    }
}
