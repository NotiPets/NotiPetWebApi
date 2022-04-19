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
    public class SalesController : ValidationControllerBase
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
                                .Include(sale => sale.Business)
                                .Include(sale => sale.Orders)
                                .ThenInclude(order => order.Appointment)
                                .ToListAsync()));
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
                string error = $"{e.Message}\n{e.InnerException}\n{e.StackTrace}";
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
        public async Task<ActionResult<JsendWrapper>> PostSale(SaleDto saleDto)
        {
            try
            {
                var error = await Validate(saleDto, new SaleValidation(_context));
                if (error == null)
                {
                    var sale = saleDto.ConvertToType();
                    sale.Orders.ToList().ForEach(o =>
                    {
                        if (o.Quantity < 1)
                        {
                            o.Quantity = 1;
                        }
                    });
                    sale.Total = await ComputeSaleTotal(sale.Orders.ToList());
                    _context.Sales.Add(sale);
                    await _context.SaveChangesAsync();
                    return Ok(new JsendSuccess(sale));
                }
                return error;
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
