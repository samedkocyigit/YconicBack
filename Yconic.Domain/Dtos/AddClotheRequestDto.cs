using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos
{
    public class AddClotheRequestDto
    {
        public string Name { get; set; }  
        public string? Brand { get; set; }  
        public Guid CategoryId { get; set; }  
        public List<IFormFile> Photos { get; set; }  
    }
}
