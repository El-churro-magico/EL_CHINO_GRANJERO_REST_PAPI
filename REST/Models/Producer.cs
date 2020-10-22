using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar un productor.
    /// </summary>
    public class Producer
    {
        public int cedula { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string businessName { get; set; }
        public string province { get; set; }
        public string canton { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public int phoneN { get; set; }
        public string birthDate { get; set; }
        public int sinpeN { get; set; }
        private string password { get; set; }
        public string deliveryPlaces { get; set; }
        public float calification { get; set; }
        public ArrayList products { get; set; } 
        public string image { get; set; }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="cedula">Cédula.</param>
        /// <param name="name">Nombre.</param>
        /// <param name="lastName">Apellidos.</param>
        /// <param name="province">´Provincia.</param>
        /// <param name="canton">Cantón.</param>
        /// <param name="district">Distrito.</param>
        /// <param name="address">Dirección.</param>
        /// <param name="phoneN">Teléfono.</param>
        /// <param name="birthDate">Fecha de nacimiento.</param>
        /// <param name="sinpeN">Número de Sinpe Móvil.</param>
        /// <param name="calification">Calificación.</param>
        /// <param name="deliveryPlaces">Lugares de entrega.</param>
        /// <param name="businessName">Nombre del negocio.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="image">Imagen.</param>
        public Producer(int cedula,string name,string lastName,string province,string canton,string district,string address,int phoneN,string birthDate,int sinpeN,float calification,string deliveryPlaces,string businessName,string password,string image)
        {
            this.cedula = cedula;
            this.name = name;
            this.lastName = lastName;
            this.businessName = businessName;
            this.province = province;
            this.canton = canton;
            this.district = district;
            this.address = address;
            this.phoneN = phoneN;
            this.birthDate = birthDate;
            this.sinpeN = sinpeN;
            this.password = password;
            this.deliveryPlaces = deliveryPlaces;
            this.calification = calification;
            this.image = image;
        }

        /// <summary>
        /// Método para obtener la contraseña del productor.
        /// </summary>
        /// <returns> Retorna la contraseña del productor.</returns>
        public string getPassword()
        {
            return this.password;
        }

        /// <summary>
        /// Método para asignar los productos del productor.
        /// </summary>
        /// <param name="products"> Lista de productos.</param>
        public void setProducts(ArrayList products)
        {
            this.products = products;
        }
    }
}