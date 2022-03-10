using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [Column("DocumentTypeId")]
        public DocumentTypeId DocumentType { get; set; }

        [Required]
        [StringLength(16)]
        public string? Document { get; set; }

        [Required]
        [StringLength(20)]
        public string? Names { get; set; }

        [Required]
        [StringLength(20)]
        public string? Lastnames { get; set; }

        [Required]
        [StringLength(15)]
        public string? Phone { get; set; }

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

        [Column("RoleId")]
        [Required]
        public RoleId Role { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [NotMapped]
        public Business? Business { get; set; }

        public bool Active { get; set; } = true;

        [Required]
        [StringLength(30)]
        public string? Username { get; set; }

        [Required]
        [StringLength(128)]
        public string? Password { get; set; }

        [Required]
        [StringLength(320)]
        public string? Email { get; set; }

        [StringLength(320)]
        public string? Description { get; set; }

        public string? PictureUrl { get; set; }

        [Column("SpecialityId")]
        public SpecialityId Speciality { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; } = DateTime.Now;
    }
}
