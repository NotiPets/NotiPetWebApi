using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Sale
    {
        public Guid Id { get; set; }
        [Required]
        public int BusinessId { get; set; }
        public Business? Business { get; set; }
        public decimal Total { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = DateTime.UtcNow;
        public ICollection<Order> Orders { get; set; }
    }
}
