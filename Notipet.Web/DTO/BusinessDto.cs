using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class BusinessDto
    {
        public string? BusinessName { get; set; }
        public string? Rnc { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }

        public Business ConvertToType()
        {
            return new Business()
            {
                Id = new Guid(),
                BusinessName = BusinessName,
                Rnc = Rnc,
                Phone = Phone,
                Email = Email,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                Province = Province
            };
        }
    }
}
