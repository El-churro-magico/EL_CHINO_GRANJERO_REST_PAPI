using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar una notificación.
    /// </summary>
    public class Notification
    {
        public int ID { get; set; }
        public int producerID { get; set; }
        public int clientID { get; set; }
        public string message { get; set; }

       /// <summary>
       /// Constructor de la clase.
       /// </summary>
       /// <param name="id">.Identificador único.</param>
       /// <param name="producerid">Identificador de productor.</param>
       /// <param name="clientid">Identificador de cliente.</param>
       /// <param name="message">Mn¡ensaje contenido en la notificación.</param>
        public Notification (int id,int producerid,int clientid,string message)
        {
            this.ID = id;
            this.producerID = producerid;
            this.clientID = clientid;
            this.message = message;
        }
    }
}