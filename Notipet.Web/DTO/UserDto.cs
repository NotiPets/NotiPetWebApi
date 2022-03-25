using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class UserDto : IConvertToType<User>
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
        public RoleId Role { get; set; }
        public int BusinessId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PictureUrl { get; set; }

        public User ConvertToType()
        {
            return new User()
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
                Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Role = Role,
                BusinessId = BusinessId,
                Username = Username,
                Password = Password,
                PictureUrl = PictureUrl,
                Email = Email
            };
        }

    }
}
