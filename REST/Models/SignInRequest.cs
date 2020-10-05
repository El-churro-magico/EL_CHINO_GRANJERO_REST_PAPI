using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class SignInRequest:SignOutRequest
    {
        public string password { get; set; }

        public SignInRequest(string type,int id,string password):base(type,id)
        {
            this.password = password;
        }
    }
}