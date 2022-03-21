using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Specialist
    {
        // This really needs to be a Guid, but that could be a problem
        // https://stackoverflow.com/questions/36022901/how-can-i-change-an-int-id-column-to-guid-with-ef-migration
        public int Id { get; set; }
        public User? User { get; set; }
        public int SpecialityId { get; set; }
        [NotMapped]
        public Speciality? Speciality { get; set; }
    }
}
