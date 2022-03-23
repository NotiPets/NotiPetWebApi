using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Specialist
    {
        public Guid Id { get; set; }

        public User? User { get; set; }

        public Speciality? Speciality { get; set; }
    }
}
