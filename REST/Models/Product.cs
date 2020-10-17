using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class Product
    {

        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int producer { get; set; }
        public string image { get; set; }
        public float cost { get; set; }
        public string saleMode{ get; set; }
        public float inStock { get; set; }
        public int quantity{ get; set; }

        public Product(int id, string name, string category, int producer, string image, float cost, string saleMode, float inStock,int quantity)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.producer = producer;
            this.image = image;
            this.cost = cost;
            this.saleMode = saleMode;
            this.inStock = inStock;
            this.quantity = quantity;
        }

    }
}