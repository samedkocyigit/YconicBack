using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos;

public class PatchClotheRequestDto
{
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
}
