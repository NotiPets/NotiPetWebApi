using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class TicketDto : IConvertToType<Ticket>
    {

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public Ticket ConvertToType()
        {
            return new Ticket()
            {
                Id = new Guid(),
                Title = Title,
                Name = Name,
                Phone = Phone,
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Email = Email,
                Description = Description
            };
        }
    }
}
