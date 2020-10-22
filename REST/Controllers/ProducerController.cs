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
    /// <summary>
    /// Clase controladora de Producer
    /// </summary>
    public class ProducerController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

<<<<<<< HEAD
        // GET: api/Producer
        /// <summary>
        /// Método para obtener todos los productores que se encuentran en la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de productores.</returns>
=======
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
>>>>>>> camacho
        public ArrayList Get()
        {
            return dbConnection.getAllProducers();
        }
        // GET: api/Producer/5
        /// <summary>
        /// Método para obtener un productor según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna un productor,</returns>
        public Producer Get(int id)
        {
            return dbConnection.getProducer(id);
        }

        /// <summary>
        /// Método para obtener todos los producotres de una ubicación dada.
        /// </summary>
        /// <param name="province">Provincia.</param>
        /// <param name="canton">Cantón.</param>
        /// <param name="district">Distrito.</param>
        /// <returns>Retorna lista de productores.</returns>
        [Route ("api/Producer/getProducerByLocation/{province}/{canton}/{district}")]
        public ArrayList Get(string province,string canton, string district)
        {
            return dbConnection.productAsigner(dbConnection.getProducersByLocation(province,canton,district));
        }

<<<<<<< HEAD
        /// <summary>
        /// Método para registrar una calificación.
        /// </summary>
        /// <param name="rating">Calificación.</param>
        /// <param name="producerID">Identificador del productor.</param>
        /// <param name="notificationID">Identificador de notificación.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
=======
        [Route("api/Producer/getUserByUserName/{userName}")]
        public Producer POST([FromBody] Token token, string userName)
        {
            return dbConnection.getProducerbyId(token.token, userName);
        }

>>>>>>> camacho
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
<<<<<<< HEAD
        /// <summary>
        /// Método para actualizar un productor, en la base de datos, según identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <param name="value">Productor con datos actualizados.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
=======
        [Route("api/Producer/{id}")]
>>>>>>> camacho
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
<<<<<<< HEAD
        /// <summary>
        /// Método para eliminar un productor según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
=======
        [Route("api/Producer/{id}")]
>>>>>>> camacho
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
