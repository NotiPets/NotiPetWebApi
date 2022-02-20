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
        [Column("UserRoleId")]
        public UserRole? UserRole { get; set; }

        [Required]
        [Column("AssetsServicesId")]
        public AssetsServices? AssetsServices { get; set; }

        [Required]
        public int RatingNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
