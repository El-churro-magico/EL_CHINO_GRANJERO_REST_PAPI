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

        [Route("api/Producer/getTops/{type}/{id}")]
        public ArrayList Get(string type,int id)
        {
            if (type.Equals("SP"))
            {
                return dbConnection.getTop10SoldProducts();
            }
            else if(type.Equals("CG"))
            {
                return dbConnection.getTop10MostProfitableProducts();
            }
            else if(type.Equals("TB"))
            {
                return dbConnection.getTop10BestBuyers();
            }
            else if(type.Equals("TPP"))
            {
                return dbConnection.getProducerTop10SoldProducts(id);
            }
            return null;
        }
        [Route("api/Producer")]
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

        [Route("api/Producer/getUserByUserName/{userName}")]
        public Producer POST([FromBody] Token token, string userName)
        {
            return dbConnection.getProducerbyId(token.token, userName);
        }

        [Route("api/Producer/Rate/{rating}/{producerID}/{notificationID}")]
        public HttpResponseMessage Post(int rating,int producerID,int notificationID)
        {
            int response=dbConnection.postRating(rating,producerID,notificationID);

            if (response == 409)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "La calificacion no pudo ser creada");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Califiacion creada correctamente!");
        }
  
        // PUT: api/Producer/5
        [Route("api/Producer/{id}")]
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
        [Route("api/Producer/{id}")]
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
