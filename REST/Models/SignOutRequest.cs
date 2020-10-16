using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class SignOutRequest
    {
        public string type { get; set; }
        public string token { get; set; }

        public SignOutRequest(string type,string token)
        {
            this.type = type;
            this.token = token;
        }
    }
}