using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace REST.Models
{
    public class Order
    {
        public int orderId { get; set;}
        public List<List<int>> productIds { get; set; }
        public int clientID { get; set; }
        public string invoice { get; set; }
        public string token { get; set; }
        public string address { get; set; }

        public Order(int clientid,string invoice,string token,List<List<int>>productIds,string address)
        {
            this.clientID = clientid;
            this.invoice = invoice;
            this.token = token;
            this.productIds = productIds;
            this.address = address;
        }


    }
}