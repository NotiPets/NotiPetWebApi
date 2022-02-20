using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class AssetsServices
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;

        [Column("VendorId")]
        public VendorId Vendor { get; set; }
        public bool Active { get; set; } = true;

        [Required]
        [Column("AssetsServiceTypeId")]
        public AssetsServiceTypeId AssetsServiceType { get; set; }

        [Required]
        [Column("BusinessId")]
        public Business Business { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
