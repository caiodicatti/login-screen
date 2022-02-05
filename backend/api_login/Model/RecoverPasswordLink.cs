using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Model
{
    public class RecoverPasswordLink
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Code { get; set; }
    }
}
