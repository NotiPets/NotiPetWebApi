using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        [Column("UserRoleId")]
        public UserRole UserRole { get; set; }

        [Required]
        [Column("AssetsServicesId")]
        public AssetsServices AssetsAssetsServices { get; set; }

        [Required]
        [Column("SaleId")]
        public Sale Sale { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column("OrderStatusId")]
        public OrderStatusId OrderStatus { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
