using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class SpecialistSignUpDto
    {
        public UserDto User { get; set; }
        public int SpecialtyId { get; set; }
        public Specialist CovertToType()
        {
            return new Specialist
            {
                User = User.CovertToType(),
                SpecialityId = SpecialtyId
            };
        }
    }
}
