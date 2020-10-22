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
    /// Clase controladora de Affilliation Form.
    /// </summary>
    public class AffilliationController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        // GET: api/Affilliation
        /// <summary>
        /// Método para enviar todas las solicitudes de afiliación.
        /// </summary>
        /// <returns>Retorna una lista de solicitudes de afiliación.</returns>
        public ArrayList Get()
        {
            return dbConnection.getAllAffilliationForms();
        }

        // GET: api/Affilliation/5
        /// <summary>
        /// Método para obtener una solicitud de afiliación según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna una solicitud de afiliación.</returns>
        public AffilliationForm Get(int id)
        {
            return dbConnection.getAffilliationForm(id);
        }

        // POST: api/Affilliation
        /// <summary>
        /// Método para crear una solicitud de afiliación en la base de datos.
        /// </summary>
        /// <param name="value">Solicitud de afiliación por crear</param>
        /// <returns>Retorna una respuesta http que permite verificar el estado de la operación</returns>
        public HttpResponseMessage Post([FromBody]AffilliationForm value)
        {
            string status = dbConnection.saveAffiliationForm(value);
            if(!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created,"Solicitud de afiliacion creada correctamente!");
        }

        // PUT: api/Affilliation/5
        /// <summary>
        /// Método para actualizar una solicitud de afiliación según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <param name="value">Valores por actualizar.</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            string result = dbConnection.updateAffiliationForm(id, value);
            if(result.Equals("200"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Operacion realizada con exito!");
            }
            else if(result.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Solicitud de afiliacion no encontrada");
            }
            return null;
        }
    }
}
