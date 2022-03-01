using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class UserDto
    {
        public DocumentTypeId DocumentType { get; set; }
        public string? Document { get; set; }
        public string? Names { get; set; }
        public string? Lastnames { get; set; }
        public string? Phone { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }

        public User CovertToType()
        {
            return new User
            {
                Id = new Guid(),
                DocumentType = DocumentType,
                Document = Document,
                Names = Names,
                Lastnames = Lastnames,
                Phone = Phone,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                Province = Province,
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };
        }

    }
}
