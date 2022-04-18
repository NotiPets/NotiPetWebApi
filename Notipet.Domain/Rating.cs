using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Rating
    {
        public Guid Id { get; set; }

        [Required]
        public int BusinessId { get; set; }

        public Business? Business { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }

        [Required]
        public int RatingNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
