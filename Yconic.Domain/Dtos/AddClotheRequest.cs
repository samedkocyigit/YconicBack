using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos
{
    public class AddClotheRequest
    {
        public string Name { get; set; }  // Required name
        public string? Brand { get; set; }  // Optional brand
        public Guid CategoryId { get; set; }  // Required category
        public List<IFormFile> Photos { get; set; }  // Required photos
    }

}
