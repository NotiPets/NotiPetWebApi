using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class PetDto : IConvertToType<Pet>
    {

        public string? Name { get; set; }

        public PetTypeId PetType { get; set; }

        public Guid UserId { get; set; }

        public SizeId Size { get; set; } = 0;

        public string? PictureUrl { get; set; }

        public string? Description { get; set; }

        public bool Gender { get; set; }

        public bool Vaccinated { get; set; } = false;
        public bool Castrated { get; set; } = false;
        public bool HasTracker { get; set; } = false;
        public DateTime Birthdate { get; set; }

        public Pet ConvertToType()
        {
            Pet p = new Pet();
            p.Name = Name;
            p.PetType = PetType;
            p.UserId = UserId;
            p.Size = Size;
            p.PictureUrl = PictureUrl;
            p.Description = Description;
            p.Gender = Gender;
            p.Vaccinated = Vaccinated;
            p.Castrated = Castrated;
            p.HasTracker = HasTracker;
            p.Birthdate = Birthdate.ToUniversalTime();
            return p;
        }
    }
}
