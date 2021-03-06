using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class OrderDto : IConvertToType<Order>
    {
        public Guid UserId { get; set; }
        public int AssetsServicesId { get; set; }
        public AppointmentDto? Appointment { get; set; }
        public int Quantity { get; set; }
        public Guid PetId { get; set; }
        public Order ConvertToType()
        {
            return new Order
            {
                UserId = UserId,
                AssetsServicesId = AssetsServicesId,
                Appointment = Appointment?.ConvertToType(),
                Quantity = Quantity,
                PetId = PetId
            };
        }
    }
}
