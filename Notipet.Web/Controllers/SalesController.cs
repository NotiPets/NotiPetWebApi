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
    public class SalesController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public SalesController(NotiPetBdContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales(Guid userId)
        {
            try
            {
                return Ok(new JsendSuccess(await _context.Sales
                                .Where(sale => sale.Orders.Where(order => order.UserId == userId).Any())
                                .Include(sale => sale.Orders)
                                .ThenInclude(order => order.Appointment)
                                .ToListAsync()));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }


        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(Guid id)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);
                if (sale == null)
                {
                    return NotFound(new JsendFail(new { }));
                }
                return Ok(new JsendSuccess(sale));
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(SaleDto saleDto)
        {
            try
            {
                var sale = saleDto.ConvertToType();
                if (!(await AppointmentInAssetOrder(sale.Orders.ToList())))
                {
                    sale.Total = await ComputeSaleTotal(sale.Orders.ToList());
                    _context.Sales.Add(sale);
                    await _context.SaveChangesAsync();
                    return Ok(new JsendSuccess(sale));
                }
                else
                {
                    return BadRequest(new JsendFail(new { appointment = "Asset order can't have an appointment" }));
                }
            }
            catch (Exception e)
            {
                string error = $"{e.Message}\n{e.StackTrace}";
                if (Methods.IsDevelopment())
                {
                    return StatusCode(500, new JsendError(error));
                }
                Console.WriteLine(error);
                return StatusCode(500, new JsendError(Constants.ControllerTextResponse.Error));
            }

        }

        private async Task<bool> AppointmentInAssetOrder(List<Order> orders)
        {
            foreach (var order in orders)
            {
                var assetService = await _context.AssetsServices.Where(x => x.Id == order.AssetsServicesId).FirstOrDefaultAsync();
                if (assetService.AssetsServiceType == AssetsServiceTypeId.Product && order.Appointment != null)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<decimal> ComputeSaleTotal(List<Order> orders)
        {
            decimal total = 0;
            foreach (var order in orders)
            {
                var price = await _context.AssetsServices.Where(x => x.Id == order.AssetsServicesId).Select(x => x.Price).FirstOrDefaultAsync();
                total += price * order.Quantity;
            }
            return total;
        }

    }
}
