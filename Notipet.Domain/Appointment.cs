using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Appointment
    {
        // This needs to be a Guid
        public Guid Id { get; set; }
        public Guid? SpecialistId { get; set; }

        [NotMapped]
        public Specialist Specialist { get; set; }

        [Required]
        [Column("AppointmentStatusId")]
        public AppointmentStatusId AppointmentStatus { get; set; } = AppointmentStatusId.Requested;
        public bool IsEmergency { get; set; } = false;
        public bool Active { get; set; } = true;
        [Required]
        public DateTime Date { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = DateTime.UtcNow;
    }
}
