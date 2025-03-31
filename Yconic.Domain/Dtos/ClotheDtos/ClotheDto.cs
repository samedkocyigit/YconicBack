using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClothePhotoDtos;

namespace Yconic.Domain.Dtos.ClotheDtos
{
    public class ClotheDto
    {
        public Guid id { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string mainPhoto { get; set; }
        public Guid categoryId { get; set; }
        public List<ClothePhotoDto> photos { get; set; }
    }
}
