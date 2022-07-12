using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Business
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? BusinessName { get; set; }

        [Required]
        [StringLength(20)]
        public string? Rnc { get; set; }

        [Required]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(320)]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Address1 { get; set; }

        [StringLength(100)]
        public string? Address2 { get; set; }

        [Required]
        [StringLength(25)]
        public string? City { get; set; }

        [Required]
        [StringLength(25)]
        public string? Province { get; set; }

        [StringLength(2048)]
        public string? PictureUrl { get; set; }

        [Required]
        public double? Latitude { get; set; }

        [Required]
        public double? Longitude { get; set; }

        [StringLength(5000)]
        public string? Comment { get; set; } = null;

        [Required]
        public DateTime OpeningTime { get; set; }

        [Required]
        public DateTime ClosingTime { get; set; }
    }
}
