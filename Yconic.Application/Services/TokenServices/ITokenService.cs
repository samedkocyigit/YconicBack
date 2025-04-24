using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models.UserModels;

namespace Yconic.Application.Services.TokenServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
