﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.ClothePhotoDtos
{
    public class AddClothePhotosDto
    {
        public List<IFormFile> Photos { get; set; }
    }
}
