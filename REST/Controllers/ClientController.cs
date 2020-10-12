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
    public class ClientController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        // GET: api/Client
        public ArrayList Get()
        {
            return dbConnection.getAllClients();
        }

        [Route("api/Client/test/{name}")]
        public string Get(string name){
            return "Ok PAPEh";
        }

        // GET: api/Client/5
        public Client Get(int id)
        {
            return dbConnection.getClient(id);
        }

        // POST: api/Client
        public HttpResponseMessage Post([FromBody]Client value)
        {
            int response = dbConnection.createClient(value);
            if (response == 409)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "La cedula proporcionada ya esta registrada!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Cliente creado correctamente");
        }

        // PUT: api/Client/5
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

        // DELETE: api/Client/5
        public HttpResponseMessage Delete(int id)
        {
            int response = dbConnection.deleteClient(id);
            if (response != 404)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Cliente eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el cliente solicitado");
        }
    }
}
