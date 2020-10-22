using System;
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
    /// Clase controladora de SignInRequest.
    /// </summary>
    public class SignInController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        /// <summary>
        /// Método para crear o iniciar una sesión.
        /// </summary>
        /// <param name="password">Contraseña.</param>
        /// <param name="type">Tipo de usuario.</param>
        /// <param name="userName">Nombre de usuario.</param>
        /// <returns>Retorna un respuesta en formato http que permite verificar el estado de la operación.</returns>
        [Route("api/SignIn/{type}/{userName}")]
        public HttpResponseMessage Post([FromBody]SignInRequest password,string type,string userName)
        {
            string response = response = dbConnection.getToken(userName, password.password, type);
            
            if (response!="409")
            {
                return Request.CreateResponse(HttpStatusCode.OK,response);
            }
            return Request.CreateResponse(HttpStatusCode.Conflict,"Usuario o contrasena incorrecto!");
        }

        // DELETE: api/SignIn
        /// <summary>
        /// Método para cerrar o eliminar una sesión.
        /// </summary>
        /// <param name="credentials">Solicitud de cierre de sesión.</param>
        /// <returns>Retorna un respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Delete([FromBody]SignOutRequest credentials)
        {
            HttpResponseMessage response;
            if (dbConnection.logOut(credentials))
            {
                response = Request.CreateResponse(HttpStatusCode.OK,"Log Out realizado satisfactoriamente!");
                return response;
            }
            response = Request.CreateResponse(HttpStatusCode.NotFound,"ID no encontrado");
            return response;
        }
    }
}
