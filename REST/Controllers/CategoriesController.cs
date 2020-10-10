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
    public class CategoriesController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();


        // GET: api/Categories
        public ArrayList Get()
        {
            return dbConnection.getAllCategories();
        }

        // GET: api/Categories/5
        public Categorie Get(int id)
        {
            return dbConnection.getCategorie(id);
        }

        // POST: api/Categories
        public HttpResponseMessage Post([FromBody]Categorie value)
        {
            int response = dbConnection.createCategorie(value);
            if(response==409)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "El nombre o ID proporcionados ya existen!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Categoria creada correctamente");
        }

        // PUT: api/Categories/5
        public HttpResponseMessage Put(int id, [FromBody]Categorie value)
        {
            int response = dbConnection.updateCategorie(id, value);
            if (response == 200)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Categoria actualizada correctamente");
            }
            else if (response == 404)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Categoria no encontrada");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "El numero de ID o nombre que se quiere establecer ya esta registrado en otra categoria!");
        }

        // DELETE: api/Categories/5
        public HttpResponseMessage Delete(int id)
        {
            int response = dbConnection.deleteCategorie(id);
            if (response!=404)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Categoria eliminada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar la categoria solicitada");
        }
    }
}
