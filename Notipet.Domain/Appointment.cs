using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [Column("SpecialistId")]
        public Specialist Specialist { get; set; }

        [Required]
        [Column("AppointmentStatusId")]
        public AppointmentStatusId AppointmentStatus { get; set; }
        public bool IsEmergency { get; set; } = false;
        public bool Active { get; set; } = true;
        [Required]
        public DateTime Date { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
