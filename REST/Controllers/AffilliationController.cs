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
    public class AffilliationController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        // GET: api/Affilliation

        public ArrayList Get()
        {
            return dbConnection.getAllAffilliationForms();
        }

        // GET: api/Affilliation/5
        public AffilliationForm Get(int id)
        {
            return dbConnection.getAffilliationForm(id);
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
