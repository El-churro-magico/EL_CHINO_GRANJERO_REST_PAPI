using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public int producerID { get; set; }
        public int clientID { get; set; }
        public string message { get; set; }

        public Notification (int id,int producerid,int clientid,string message)
        {
            this.ID = id;
            this.producerID = producerid;
            this.clientID = clientid;
            this.message = message;
        }
    }
}