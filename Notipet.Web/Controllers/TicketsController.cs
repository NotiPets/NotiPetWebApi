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
    public class TicketsController : ControllerBase
    {
        private readonly NotiPetBdContext _context;

        public TicketsController(NotiPetBdContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<JsendWrapper>> PostTicket(TicketDto ticketDto)
        {
            try
            {
                var ticket = ticketDto.ConvertToType();
                _context.Ticket.Add(ticket);
                await _context.SaveChangesAsync();
                return Ok(new JsendSuccess(ticket));
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
        [HttpGet]
        public async Task<ActionResult<JsendWrapper>> GetTicket() => Ok(new JsendSuccess(await _context.Ticket.ToListAsync()));
    }
}
