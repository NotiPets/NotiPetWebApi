using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public enum RoleId
    {
        Client,
        Seller,
        Admin
    }

    // This class just exists to create an equivalent table to the above enum
    public class Role
    {
        public RoleId Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        //Pretty much useless, just to follow EF core conventions and create the Foreign Key
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
