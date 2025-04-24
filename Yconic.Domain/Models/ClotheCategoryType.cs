using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class ClotheCategoryType : BaseClass
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
        public ICollection<ClotheCategory>? ClothesCategory { get; set; } = new List<ClotheCategory>();
    }
}