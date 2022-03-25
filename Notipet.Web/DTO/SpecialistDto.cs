using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class SpecialistDto : IConvertToType<Specialist>
    {
        public UserDto User { get; set; }
        public int SpecialtyId { get; set; }
        public Specialist ConvertToType()
        {
            return new Specialist
            {
                User = User.ConvertToType(),
                SpecialityId = SpecialtyId
            };
        }
    }
}
