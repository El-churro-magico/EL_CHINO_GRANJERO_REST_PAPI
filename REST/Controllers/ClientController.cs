using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using REST.Models;

namespace REST.Controllers
{
    /// <summary>
    /// Clase controladora de client.
    /// </summary>
    public class ClientController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        // GET: api/Client
        /// <summary>
        /// Método para obtener todos los clientes existentes en la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de clientes.</returns>
        public ArrayList Get()
        {
            return dbConnection.getAllClients();
        }

        /// <summary>
        /// Método para obtener un cliente según un nombre de usuario dado.
        /// </summary>
        /// <param name="token">Token del cliente.</param>
        /// <param name="userName">Nombre de usuario del cliente.</param>
        /// <returns>Retorna un cliente.</returns>
        [Route("api/Client/getUserByUserName/{userName}")]
        public Client POST([FromBody]Token token,string userName)
        {
            return dbConnection.getClientbyUserName(token.token, userName);
        }

        // GET: api/Client/5
        /// <summary>
        /// Método para obtener un cliente según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna un cliente.</returns>
        public Client Get(int id)
        {
            return dbConnection.getClient(id);
        }

        // POST: api/Client
        /// <summary>
        /// Método para crear un cliente en la base de datos.
        /// </summary>
        /// <param name="value">Cliente por crear.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Post([FromBody]Client value)
        {
            int response = dbConnection.createClient(value);
            if (response == 409)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "La cedula o el nombre de usuario proporcionados ya estan registrados!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Cliente creado correctamente");
        }

        // PUT: api/Client/5
        /// <summary>
        /// Método para actualizar un cliente, en la base de datos, según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <param name="value">Cliente con datos actualizados.</param>
        /// <returns>Retorna un respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Put(int id, [FromBody]Client value)
        {
            int response = dbConnection.updateClient(id, value);
            if (response == 200)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Cliente actualizado correctamente");
            }
            else if (response == 404)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cliente no encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.Conflict, "El numero de cedula que se quiere establecer ya ha sido registrado por otro cliente!");
        }

        // DELETE: api/Client
        /// <summary>
        /// Método para eliminar un ciente de la base de datos según un token dado.
        /// </summary>
        /// <param name="token">Token del cliente por eliminar.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Delete([FromBody]Token token)
        {
            int response = dbConnection.deleteClient(token.token);
            if (response != 404)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Cliente eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el cliente solicitado");
        }
    }
}
