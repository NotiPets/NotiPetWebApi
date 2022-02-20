using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public enum AssetsServiceTypeId
    {
        Product,
        Service
    }
    public class AssetsServiceType
    {
        public AssetsServiceTypeId Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        //Pretty much useless, just to follow EF core conventions and create the Foreign Key
        public ICollection<AssetsServices> AssetsServices { get; set; }
    }
}
