using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class Token
    {
        public string token { get; set; }

        public Token(string token)
        {
            this.token = token;
        }
    }
}