using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Pet
    {
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(20)]
        public string? Name { get; set; }

        [Required]
        [Column("PetTypeId")]
        public PetTypeId PetType { get; set; }

        [NotMapped]
        public string? PetTypeName { get; set; }

        // Maybe change all of this to Owner
        [Required]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [Column("SizeId")]
        public SizeId? Size { get; set; } = SizeId.Small;
        public bool Active { get; set; } = true;

        [StringLength(2048)]
        public string? PictureUrl { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        // Male by default
        public bool Gender { get; set; } = true;

        // Is this really necesary?
        public bool Vaccinated { get; set; } = false;
        public bool Castrated { get; set; } = false;
        public bool HasTracker { get; set; } = false;
        public DateTime Birthdate { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = DateTime.UtcNow;
    }
}
