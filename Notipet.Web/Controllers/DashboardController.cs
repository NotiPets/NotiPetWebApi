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

        [HttpGet("AppointmentCompleted")]
        public async Task<ActionResult<JsendWrapper>> AppointmentCompleted() => Ok(new JsendSuccess(await _context.Appointments.Where(x => x.AppointmentStatus == AppointmentStatusId.Completed).ToListAsync()));

        [HttpGet("AppointmentPending")]
        public async Task<ActionResult<JsendWrapper>> AppointmentPending() => Ok(new JsendSuccess(await _context.Appointments.Where(x => x.AppointmentStatus == AppointmentStatusId.Requested).ToListAsync()));

        [HttpGet("AppliedVaccines")]
        public async Task<ActionResult<JsendWrapper>> AppliedVaccines() => Ok(new JsendSuccess(await _context.DigitalVaccines.ToListAsync()));

        [HttpGet("Last7DaysAppointments")]
        public async Task<ActionResult<JsendWrapper>> Last7DaysAppointments() => Ok(new JsendSuccess(await _context.Appointments.Where(x => x.Created >= DateTime.UtcNow.AddDays(-7)).ToListAsync()));

    }
}
