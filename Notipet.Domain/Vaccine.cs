using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public  class Vaccine
    {
        public Guid Id { get; set; }

        [Required]
        [Column("PetTypeId")]
        public PetType? PetType { get; set; }

        [Required]
        [StringLength(30)]
        public string? VaccineName { get; set; }

        [Required]
        [Column("BusinessId")]
        public Business? Business { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
