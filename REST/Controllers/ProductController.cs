using REST.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    /// <summary>
    /// Clase que controla Product
    /// </summary>
    public class ProductController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        // GET: api/Product
        /// <summary>
        /// Método para obtener todos los productos de un productor o un top dependiendo de la entrada.
        /// </summary>
        /// <param name="cedula">Cédula del productor.</param>
        /// <param name="top">Top solicitado.</param>
        /// <returns>Retorna una lista de productos.</returns>
        public ArrayList Get(int cedula, string top)
        {
            if (cedula.Equals(null) || cedula.Equals(""))
            {
                if(top.Equals("SOLD"))
                {
                    return dbConnection.getTop10SoldProducts();
                }
                else if(top.Equals("PROFITS"))
                {
                    return dbConnection.getTop10MostProfitableProducts();
                }
                return null;
            }
            else
            {
                if (top.Equals(null) || top.Equals(""))
                {

                    return dbConnection.getProducerAllProducts(cedula);
                }
                else if (top.Equals("SOLD"))
                {
                    return dbConnection.getProducerTop10SoldProducts(cedula);
                }
                return null;
            }
        }
        
        /// <summary>
        /// Método para obtener un productor según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna un producto.</returns>
        public Product Get(int id)
        {
            return dbConnection.getProduct(id);
        }

        // POST: api/Product
        /// <summary>
        /// Método para crear un producto en la base de datos.
        /// </summary>
        /// <param name="value">Producto con datos actualizados.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Post([FromBody]Product value)
        {
            string status = dbConnection.createProduct(value);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "Producto creado correctamente!");
        }

        // PUT: api/Product/5
        /// <summary>
        /// Método para actualizar producto, en la base de datos, según identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <param name="value">Producto con datos actualizados.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Put(int id, [FromBody]Product value)
        {
            string response = dbConnection.updateProduct(id, value);
            if (response.Equals("200"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Producto actualizado correctamente");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Producto no encontrado");
        }

        // DELETE: api/Product/5
        /// <summary>
        /// Método para eliminar un producto de la base de  datos.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Delete(int id)
        {
            string response = dbConnection.deleteProduct(id);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Producto eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el producto solicitado");
        }

    }
}
