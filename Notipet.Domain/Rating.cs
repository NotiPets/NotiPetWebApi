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

        [Column("BusinessId")]
        public Business? Business { get; set; }

        [Required]
        [Column("UserId")]
        public User? User { get; set; }

        [Required]
        [Column("AssetsServicesId")]
        public AssetsServices? AssetsServices { get; set; }

        [Required]
        public int RatingNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
