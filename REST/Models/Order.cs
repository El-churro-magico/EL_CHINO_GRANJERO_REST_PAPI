using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace REST.Models
{
    /// <summary>
    /// Clase que represemta un pedido.
    /// </summary>
    public class Order
    {
        public int orderId { get; set;}
        public List<List<int>> productIds { get; set; }
        public int clientID { get; set; }
        public string invoice { get; set; }
        public string token { get; set; }
        public string address { get; set; }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="clientid">Identificador de cliente.</param>
        /// <param name="invoice">Factura.</param>
        /// <param name="token">Token.</param>
        /// <param name="productIds">Lista de identificadores de productos.</param>
        /// <param name="address">Dirección.</param>
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