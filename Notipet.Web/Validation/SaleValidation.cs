using Microsoft.EntityFrameworkCore;
using Notipet.Data;
using Notipet.Domain;
using Notipet.Web.DataWrapper;
using Notipet.Web.DTO;

namespace Notipet.Web.Validation
{
    public class SaleValidation : ValidationBase
    {
        public SaleValidation(NotiPetBdContext context) : base(context)
        {

        }
        public async Task<ActionResult<JsendWrapper>?> UserDoesExist(SaleDto saleDto)
        {
            if (await _context.Users.Where(x => x.Id == saleDto.Orders.ToList()[0].UserId).AnyAsync())
            {
                return null;
            }
            return Conflict(new JsendFail(new { user = "user doesn't exists" }));
        }

        public async Task<ActionResult<JsendWrapper>?> PetDoesExist(SaleDto saleDto)
        {
            if (await _context.Pets.Where(x => x.Id == saleDto.Orders.ToList()[0].PetId).AnyAsync())
            {
                return null;
            }
            return Conflict(new JsendFail(new { pet = "pet doesn't exists" }));
        }

        public async Task<ActionResult<JsendWrapper>?> AssetOrdersDoesNotHaveAppointment(SaleDto saleDto)
        {
            foreach (var order in saleDto.Orders.ToList())
            {
                var assetService = await _context.AssetsServices.FindAsync(order.AssetsServicesId);
                if (assetService?.AssetsServiceType == AssetsServiceTypeId.Product && order.Appointment != null)
                {
                    return BadRequest(new JsendFail(new
                    {
                        // Cambiar a "Order must have a service if it includes an appointment"
                        appointment = "Asset order can't have an appointment",
                        order = order
                    }));
                }
            }
            return null;
        }

        public async Task<ActionResult<JsendWrapper>?> ServiceOrdersHaveAppoitment(SaleDto saleDto)
        {
            foreach (var order in saleDto.Orders.ToList())
            {
                var assetService = await _context.AssetsServices.FindAsync(order.AssetsServicesId);
                // Si la order no es producto y tiene un appointment 
                if (assetService?.AssetsServiceType == AssetsServiceTypeId.Service && order.Appointment == null)
                {
                    return BadRequest(new JsendFail(new
                    {
                        // Cambiar a "Order must have a service if it includes an appointment"
                        appointment = "Service order must have an appointment",
                        order = order
                    }));
                }
            }
            return null;
        }

        public async Task<ActionResult<JsendWrapper>?> ServiceOrderAppoitmentHaveSpecialist(SaleDto saleDto)
        {
            foreach (var order in saleDto.Orders.ToList())
            {
                var assetService = await _context.AssetsServices.FindAsync(order.AssetsServicesId);
                // Si la order no es producto y tiene un appointment 
                if (assetService?.AssetsServiceType == AssetsServiceTypeId.Service && order?.Appointment?.SpecialistId == null)
                {
                    return BadRequest(new JsendFail(new
                    {
                        // Cambiar a "Order must have a service if it includes an appointment"
                        appointment = "The appointment must have a specialist",
                        order = order
                    }));
                }
            }
            return null;
        }

        public async Task<ActionResult<JsendWrapper>?> BusinessDoesExist(SaleDto saleDto)
        {
            if (await _context.Businesses.Where(x => x.Id == saleDto.BusinessId).AnyAsync())
            {
                return null;
            }
            return NotFound(new JsendFail(new { businness = "business doesn't exists" }));
        }
    }
}
