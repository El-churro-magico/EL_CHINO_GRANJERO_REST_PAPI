using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using REST.Models;

namespace REST.Controllers
{
    public class ProducerController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        // GET: api/Producer
        public ArrayList Get()
        {
            return dbConnection.getAllProducers();
        }

        // GET: api/Producer/5
        public Producer Get(int id)
        {
            return dbConnection.getProducer(id);
        }

        [Route ("api/Producer/getProducerByLocation/{province}/{canton}/{district}")]
        public ArrayList Get(string province,string canton, string district)
        {
            return dbConnection.productAsigner(dbConnection.getProducersByLocation(province,canton,district));
        }

        // PUT: api/Producer/5
        public HttpResponseMessage Put(int id,[FromBody]Producer value)
        {
            int response = dbConnection.updateProducer(id,value);
            if(response==200)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Productor actualizado correctamente");
            }
            else if(response==404)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Productor no encontrado");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "El numero de cedula o nombre de negocio que se quiere establecer ya fue registrado por otro productor!");
        }

        // DELETE: api/Producer/5
        public HttpResponseMessage Delete(int id)
        {
            string response = dbConnection.deleteProducer(id);
            if(!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Productor eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el productor solicitado");
        }
    }
}
