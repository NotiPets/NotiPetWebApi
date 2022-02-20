using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public enum PetTypeId
    {
        Dog,
        Cat,
        Bunny,
        Other
    }

    // This class just exists to create an equivalent table to the above enum
    public class PetType
    {
        public PetTypeId Id { get; set; }

        [Required]
        [StringLength(10)]
        public string? Name { get; set; }

        //Pretty much useless, just to follow EF core conventions and create the Foreign Key
        [ForeignKey("PetType")]
        public ICollection<Pet>? Pets { get; set; }
    }
}
