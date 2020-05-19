using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Models
{
    public class LoginResponseModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }
    }
}
