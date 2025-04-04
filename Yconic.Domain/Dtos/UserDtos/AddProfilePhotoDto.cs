using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.UserDtos
{
    public class AddProfilePhotoDto
    {
        public IFormFile photo { get; set; }
    }
}
