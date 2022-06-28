using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        [Required]
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(15)]
        public string? Phone { get; set; }
        [Required]
        [StringLength(320)]
        public string? Email { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
