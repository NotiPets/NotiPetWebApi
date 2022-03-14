using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class UserRoleDto
    {
        public UserDto? User { get; set; }
        public RoleId Role { get; set; }
        public Guid BusinessId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public User ConvertToType()
        {
            return new User()
            {
                Id = new Guid(),
                //User = User.CovertToType(),
                Role = Role,
                BusinessId = BusinessId,
                Username = Username,
                Password = Password,
                Email = Email,
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };
        }
    }
}
