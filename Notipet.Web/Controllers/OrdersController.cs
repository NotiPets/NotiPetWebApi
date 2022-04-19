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
    public class OrdersController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public OrdersController(NotiPetBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<JsendWrapper>> AllOrders() => Ok(new JsendSuccess(await _context.Orders.ToListAsync()));

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<JsendWrapper>> OrdersByUser(Guid userId) => Ok(new JsendSuccess(await _context.Orders.Where(x => x.UserId == userId).ToListAsync()));

        [HttpGet("ByBusiness/{businessId}")]
        public async Task<ActionResult<JsendWrapper>> OrdersByBusiness(int businessId) => Ok(new JsendSuccess(await _context.Sales.Where(x => x.BusinessId == businessId).Select(x => x.Orders).ToListAsync()));


        [HttpPut("CancelOrder/{id}")]
        public async Task<ActionResult<JsendWrapper>> PutOrder(Guid id)
        {
            try
            {
                if (await _context.Orders.Where(x => x.Id == id).AnyAsync())
                {
                    await UpdateOrderState(id, OrderStatusId.Cancelled);

                    return Ok(new JsendSuccess());
                }
                else
                {
                    return NotFound(new JsendFail(new { order = "order not found" }));
                }
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

        [HttpPut("Status/{id}/{status}")]
        public async Task<ActionResult<JsendWrapper>> PutOrder(Guid id, OrderStatusId status)
        {
            try
            {
                if (await _context.Orders.Where(x => x.Id == id).AnyAsync())
                {
                    await UpdateOrderState(id, status);
                    return Ok(new JsendSuccess());
                }
                else
                {
                    return NotFound(new JsendFail(new { order = "order not found" }));
                }
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

        private async Task UpdateOrderState(Guid id, OrderStatusId status)
        {
            //var order = await _context.Orders.FindAsync(id);
            var order = await _context.Orders.Where(x => x.Id == id).Include("Appointment").FirstOrDefaultAsync();
            order.OrderStatus = status;
            if (status == OrderStatusId.Cancelled)
                order.Appointment.AppointmentStatus = AppointmentStatusId.Cancelled;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
