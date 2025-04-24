using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.User;

namespace Yconic.Domain.Dtos.Auth
{
    public class LoginResponse
    {
        public string token { get; set; }
        public UserDto user { get; set; }
    }
}