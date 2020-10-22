using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar una categoría de productos.
    /// </summary>
    public class Category
    {
        public int ID { get; set; }
        public string name { get; set; }
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <param name="name">Nombre.</param>
        public Category(int id, string name)
        {
            this.ID = id;
            this.name = name;
        }
    }
}