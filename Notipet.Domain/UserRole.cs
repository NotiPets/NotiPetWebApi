using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class UserRole
    {
        public Guid Id { get; set; }

        [Column("UserId")]
        [Required]
        public User? User { get; set; }

        [Column("RoleId")]
        [Required]
        public RoleId Role { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [NotMapped]
        public Business? Business { get; set; }

        public bool Active { get; set; } = true;
        public bool Validated { get; set; } = false;

        [Required]
        [StringLength(30)]
        public string? Username { get; set; }

        [Required]
        [StringLength(128)]
        public string? Password { get; set; }

        [Required]
        [StringLength(320)]
        public string? Email { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
