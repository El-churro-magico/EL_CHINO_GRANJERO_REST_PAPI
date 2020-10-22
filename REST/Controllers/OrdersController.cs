﻿using REST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    public class OrdersController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        

        [Route("api/Orders/getProducerOrders/{producerId}/{token}")]
        public List<producerOrderView> Get(int producerId,string token)
        {
            return dbConnection.getProducerOrders(producerId, token);
        }

        // GET: api/Orders/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Orders")]
        public HttpResponseMessage Post([FromBody]Order value)
        {
            int response=dbConnection.createOrder(value);

            if (response == 200)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Pedido agregado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.Conflict, "El pedido no pudo ser agregado!");
        }
        // PUT: api/Orders/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Orders/5
        public void Delete(int id)
        {
        }
    }
}
