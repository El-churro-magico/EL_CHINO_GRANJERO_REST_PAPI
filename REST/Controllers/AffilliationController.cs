using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using REST.Models;

namespace REST.Controllers
{
    public class AffilliationController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        // GET: api/Affilliation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Affilliation/5
        public AffilliationForm Get(int id)
        {
            AffilliationForm test = new AffilliationForm(id,"Daniel", "Camacho Gonzalez", "El Chino Depravado", "Heredia", "San Rafael", "Concepcion", "del Bar La Troja, 350 m noroeste", 60131812, DateTime.Parse("01/15/1999"), 60131812,"","PENDING","1234");
            return test;
        }

        // POST: api/Affilliation
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
