using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    /// <summary>
    /// Clase controladora de Order
    /// </summary>
    public class OrdersController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        // GET: api/Orders
        /// <summary>
        /// Método para obtener un pedido.
        /// </summary>
        /// <returns>Interfaz que contiene el pedido.</returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        /// <summary>
        /// Método para obtener un pedido según un identificador dado.
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna un pedido.</returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Método para crear un pedido en la base de datos.
        /// </summary>
        /// <param name="value">Pedido por crear.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        [Route("api/Orders")]
        public HttpResponseMessage Post([FromBody]Order value)
        {
            int response=dbConnection.createOrder(value);

            if (response == 200)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Pedido agregado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.Conflict, "El pedido no pudo ser agregado!");
        }
       
        // PUT: api/Orders/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Orders/5
        public void Delete(int id)
        {
        }
    }
}
