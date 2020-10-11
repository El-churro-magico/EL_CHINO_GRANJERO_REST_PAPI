using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string name { get; set; }

        public Category(int id, string name)
        {
            this.ID = id;
            this.name = name;
        }
    }
}