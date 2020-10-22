using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{   /// <summary>
/// Clase para representar un producto
/// </summary>
    public class Product
    {

        public int id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public int producer { get; set; }
        public string image { get; set; }
        public float cost { get; set; }
        public string saleMode{ get; set; }
        public float inStock { get; set; }
        public int quantity{ get; set; }

<<<<<<< HEAD
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <param name="name">Nombre.</param>
        /// <param name="category">Categoría.</param>
        /// <param name="producer">Productor.</param>
        /// <param name="image">Imagen.</param>
        /// <param name="cost">Precio.</param>
        /// <param name="saleMode">Modo de venta.</param>
        /// <param name="inStock">En inventario.</param>
        /// <param name="quantity">Cantidad.</param>
        public Product(int id, string name, string category, int producer, string image, float cost, string saleMode, float inStock,int quantity)
=======
        public Product(string name, int category, int producer, string image, float cost, string saleMode, float inStock,int quantity)
>>>>>>> camacho
        {
            this.name = name;
            this.category = category;
            this.producer = producer;
            this.image = image;
            this.cost = cost;
            this.saleMode = saleMode;
            this.inStock = inStock;
            this.quantity = quantity;
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }
}