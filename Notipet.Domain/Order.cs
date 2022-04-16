﻿using System;
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
        public Guid UserId { get; set; }

        [NotMapped]
        public User User { get; set; }

        [Required]
        public int? AssetsServicesId { get; set; } = null;

        [NotMapped]
        public AssetsServices? AssetsServices { get; set; }

        [Required]
        public int? BusinessId { get; set; } = null;

        [NotMapped]
        public Business? Business { get; set; }

        [Column("AppointmentId")]
        public Appointment? Appointment { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column("OrderStatusId")]
        public OrderStatusId OrderStatus { get; set; } = OrderStatusId.Created;
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
