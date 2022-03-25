using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class SaleDto : IConvertToType<Sale>
    {
        public ICollection<OrderDto> Orders { get; set; }
        public Sale ConvertToType()
        {
            return new Sale
            {
                Orders = Orders.Select(x => x.ConvertToType()).ToList()
            };
        }
    }
}
