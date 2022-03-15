using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? BusinessId { get; set; }
        public string? UserId { get; set; }
    }
}
