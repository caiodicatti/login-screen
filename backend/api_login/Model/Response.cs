using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Model
{
    public class Response
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public dynamic result { get; set; }
        public string message { get; set; }
    }
}
