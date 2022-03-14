using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Domain
{
    public enum SizeId
    {
        Small,
        Medium,
        Large
    }
    public class Size
    {
        public SizeId Id { get; set; }

        [Required]
        [StringLength(10)]
        public string? Name { get; set; }

        [ForeignKey("Size")]
        public ICollection<Pet>? Pets { get; set; }
    }
}
