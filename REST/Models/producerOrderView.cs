using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class producerOrderView
    {
        public int clientId { get; set; }
        public List<Product> products { get; set; }
        public int orderId { get; set; }
        public string address { get; set; }
        public string time { get; set; }


        public producerOrderView(int clientId, List<Product> products,int orderId,string address,string time)
        {
            this.clientId = clientId;
            this.products = products;
            this.orderId = orderId;
            this.address = address;
            this.time = time;
        }
    }
}