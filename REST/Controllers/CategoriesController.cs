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
    /// Clase controladora de Category
    /// </summary>
    public class CategoriesController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();


        // GET: api/Categories
        /// <summary>
        /// Método para obtener todas las categorías existentes en la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de categorías.</returns>
        public ArrayList Get()
        {
            return dbConnection.getAllCategories();
        }

        // GET: api/Categories/5
        /// <summary>
        /// Retorna una categoría según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna una categoría.</returns>
        public Category Get(int id)
        {
            return dbConnection.getCategory(id);
        }

        // POST: api/Categories
        /// <summary>
        /// Método para crear categoría en la base de datos.
        /// </summary>
        /// <param name="value">Categoría por crear.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Post([FromBody]Category value)
        {
            int response = dbConnection.createCategory(value);
            if(response==409)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "El nombre o ID proporcionados ya existen!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Categoria creada correctamente");
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Método para actualizar una categoría, en la base de datos, según identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <param name="value">Valores por actualizar.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Put(int id, [FromBody]Category value)
        {
            int response = dbConnection.updateCategory(id, value);
            if (response == 200)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Categoria actualizada correctamente");
            }
            else if (response == 404)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Categoria no encontrada");
            }
            return Request.CreateResponse(HttpStatusCode.Conflict, "El numero de ID o nombre que se quiere establecer ya esta registrado en otra categoria!");
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Método para eliminar una categoría según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único dado.</param>
        /// <returns>Retorna una respuesta en formato http que permite verificar el estado de la operación.</returns>
        public HttpResponseMessage Delete(int id)
        {
            int response = dbConnection.deleteCategory(id);
            if (response!=404)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Categoria eliminada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar la categoria solicitada");
        }
    }
}
