using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public enum SpecialityId
    {
        Speciality1,
        Speciality2,
        Speciality3
    }

    // This class just exists to create an equivalent table to the above enum
    public class Speciality
    {
        public SpecialityId Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Name { get; set; }

        //Pretty much useless, just to follow EF core conventions and create the Foreign Key
        [ForeignKey("Speciality")]
        public ICollection<User>? Users { get; set; }
    }
}
