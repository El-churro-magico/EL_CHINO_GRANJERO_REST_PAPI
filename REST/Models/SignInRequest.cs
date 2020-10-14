using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class SignInRequest
    {
        public string password { get; set; }

        public SignInRequest(string password)
        {
            this.password = password;
        }
    }
}