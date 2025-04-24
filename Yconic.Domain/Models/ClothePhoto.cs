using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class ClothePhoto : BaseClass
    {
        public string Url { get; set; }
        public Guid ClotheId { get; set; }
        public Clothe? Clothe { get; set; }
    }
}