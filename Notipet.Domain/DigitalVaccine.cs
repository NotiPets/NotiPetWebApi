using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class DigitalVaccine
    {
        public Guid Id { get; set; }

        [Required]
        [Column("PetId")]
        public Pet Pet { get; set; }

        [Required]
        [Column("BusinessId")]
        public Business Business { get; set; }

        [Required]
        [Column("UserRoleId")]
        public UserRole UserRole { get; set; }

        [Required]
        [StringLength(30)]
        public string VaccineName { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
