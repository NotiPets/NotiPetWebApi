#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Domain;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;
using Notipet.Web.SignalR;
using Notipet.Web.Validation;
using Utilities;

namespace Notipet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ValidationControllerBase
    {
        private readonly NotiPetBdContext _context;
        private IHubContext<InformHub, IHubClient> _informHub;

        public AppointmentsController(NotiPetBdContext context, IHubContext<InformHub, IHubClient> hubContext)
        {
            _context = context;
            _informHub = hubContext;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JsendWrapper>> GetAppointment(Guid id)
        {
            try
            {
                var appointment = await _context.Appointments.Where(x => x.Id == id)
                    .Include(x => x.Specialist).ThenInclude(x => x.Speciality)
                    .Include(x => x.Specialist).ThenInclude(x => x.User).FirstOrDefaultAsync();
                if (appointment?.Specialist?.User?.Password != null)
                {
                    appointment.Specialist.User.Password = "Ignore";
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
        public async Task<ActionResult<JsendWrapper>> GetAppointmentsByBusiness(int businessId, int itemCount, int page)
        {
            try
            {
                var appointments = await _context.Sales
                    .Where(x => x.BusinessId == businessId && x.Orders.Where(x => x.AssetsServices.AssetsServiceType == AssetsServiceTypeId.Service).Any())
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.Appointment)
                    .ThenInclude(x => x.Specialist)
                    .ThenInclude(x => x.Speciality)
                    .SelectMany(x => x.Orders, (o, a) => a)
                    .OrderByDescending(x => x.Created)
                    .ToListAsync();
                var pagination = new PaginationInfo(itemCount, page, appointments.Count);
                appointments = appointments.Skip(pagination.StartAt).Take(pagination.ItemCount).ToList();
                return Ok(new JsendSuccess(new { pagination = pagination, appointments = appointments }));
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
                if (await _context.Appointments.AnyAsync(x => x.Id == id))
                {
                    var appointment = appointmentDto.ConvertToType();
                    appointment.Id = id;
                    _context.Entry(appointment).State = EntityState.Modified;
                }
                else
                {
                    return BadRequest(new JsendFail(new { appointment = "Appointment not found" }));
                }

                try
                {
                    await _context.SaveChangesAsync();
                    await _informHub.Clients.All.InformClient(Constants.SignalR.DefaultMessage);
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

        [HttpGet("TestSignalR/{param}")]
        public async Task<ActionResult<JsendWrapper>> TestSignalR(string param)
        {
            await _informHub.Clients.All.InformClient(param);
            return Ok();
        }
        private bool AppointmentExists(Guid id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
