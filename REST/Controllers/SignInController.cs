﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using REST.Models;

namespace REST.Controllers
{
    public class SignInController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        // GET: api/SignIn
        public HttpResponseMessage Get([FromBody]SignInRequest credentials)
        {
            string response = dbConnection.getToken(credentials);
            if(response!="409")
            {
                return Request.CreateResponse(HttpStatusCode.OK,response);
            }
            return Request.CreateResponse(HttpStatusCode.Conflict,"ID o contrasena incorrecto!");
        }

        // DELETE: api/SignIn/5
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
