using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.ClothePhotoDtos
{
    public class ClothePhotoDto
    {
        public Guid id { get; set; }
        public string url { get; set; }
        public Guid clotheId { get; set; }
    }
}
