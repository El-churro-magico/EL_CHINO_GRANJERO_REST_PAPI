using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class Order
    {
        public int id { get; set; }
        public int client { get; set; }
        public int producer { get; set; }
        public string products { get; set; }
        public string voucher { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        
        public Order(int id, string name, string lastName, string category, int producer, string image, float cost, string saleMode, float inStock, float profits)
        {
            this.id = id;
            this.client = client;
            this.producer = producer;
            this.voucher = voucher;
            this.producer = producer;
            this.address = address;
            this.state = state;
        }
    }
}