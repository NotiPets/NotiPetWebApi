using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class UserRoleDto
    {
        public UserDto User { get; set; }
        public RoleId Role { get; set; }
        public BusinessDto Business { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public UserRole ConvertToType()
        {
            return new UserRole()
            {
                Id = new Guid(),
                User = User.CovertToType(),
                Role = Role,
                Business = Business.ConvertToType(),
                Username = Username,
                Password = Password,
                Email = Email,
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };
        }
    }
}
