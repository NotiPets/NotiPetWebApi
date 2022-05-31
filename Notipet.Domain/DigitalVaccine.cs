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
        public Guid PetId { get; set; }

        [NotMapped]
        public Pet? Pet { get; set; }

        [Required]
        public int BusinessId { get; set; }

        [NotMapped]
        public Business? Business { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [NotMapped]
        public User? User { get; set; }

        [Required]
        public Guid VaccineId { get; set; }
        
        public Vaccine? Vaccine { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
