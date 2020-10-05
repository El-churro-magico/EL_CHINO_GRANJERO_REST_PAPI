using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class SignOutRequest
    {
        public string type { get; set; }
        public int ID { get; set; }

        public SignOutRequest(string type,int id)
        {
            this.type = type;
            this.ID = id;
        }
    }
}