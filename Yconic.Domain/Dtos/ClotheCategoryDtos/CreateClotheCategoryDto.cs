using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.ClotheCategoryDtos
{
    public class CreateClotheCategoryDto
    {
        public string Name { get; set; }
        public Guid ClotheCategoryTypeId { get; set; }
        public Guid GarderobeId { get; set; }
    }
}