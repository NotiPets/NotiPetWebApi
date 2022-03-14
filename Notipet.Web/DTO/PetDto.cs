using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class PetDto
    {

        public string? Name { get; set; }

        public PetTypeId PetType { get; set; }

        public Guid User { get; set; }

        public SizeId Size { get; set; } = 0;

        public string? PictureUrl { get; set; }

        public string? Description { get; set; }

        public bool Gender { get; set; }

        public bool Vaccinated { get; set; } = false;
        public bool Castrated { get; set; } = false;
        public bool HasTracker { get; set; } = false;
        public DateTime Birthdate { get; set; }

        public Pet ToPet()
        {
            Pet p = new Pet();
            p.Name = Name;
            p.PetType = PetType;
            p.UserId = User;
            p.Size = Size;
            p.PictureUrl = PictureUrl;
            p.Description = Description;
            p.Gender = Gender;
            p.Vaccinated = Vaccinated;
            p.Castrated = Castrated;
            p.HasTracker = HasTracker;
            p.Birthdate = Birthdate;
            return p;
        }
    }
    public class PetDto2
    {
        public Guid PetId { get; set; }
        public string? Name { get; set; }

        public string? PetType { get; set; }

        public Guid UserId { get; set; }

        public string? Size { get; set; }

        public string? PictureUrl { get; set; }

        public string? Description { get; set; }

        public bool Gender { get; set; }

        public bool Vaccinated { get; set; }
        public bool Castrated { get; set; }
        public bool HasTracker { get; set; }
        public DateTime Birthdate { get; set; }
        public bool active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public static PetDto2 Map(Pet pet)
        {
            PetDto2 p2 = new PetDto2();
            p2.PetId = pet.Id;
            p2.Name = pet.Name;
            p2.PetType = pet.PetType.ToString();
            p2.UserId = pet.UserId;
            p2.Size = pet.Size.ToString();
            p2.PictureUrl = pet.PictureUrl;
            p2.Description = pet.Description;
            p2.Gender = pet.Gender;
            p2.Vaccinated = pet.Vaccinated;
            p2.Castrated = pet.Castrated;
            p2.HasTracker = pet.HasTracker;
            p2.Birthdate = pet.Birthdate;
            p2.active = pet.Active;
            p2.Created = pet.Created;
            p2.Updated = pet.Updated;
            return p2;
        }
    }
}
