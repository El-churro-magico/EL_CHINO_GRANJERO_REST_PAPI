using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar las solicitudes de afiliación de los productores.
    /// </summary>
    public class AffilliationForm
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
        public int sinpeN{ get; set; }
        public string comment { get; set; }
        public string status { get; set; }
        public string password { get; set; }

<<<<<<< HEAD
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="cedula">Cédula.</param>
        /// <param name="name">Nombre.</param>
        /// <param name="lastName">Apellidos.</param>
        /// <param name="businessName">Nombre del negocio.</param>
        /// <param name="province">Provincia.</param>
        /// <param name="canton">Cantón.</param>
        /// <param name="district">Distrito.</param>
        /// <param name="address">Dirección.</param>
        /// <param name="phoneN">Teléfono.</param>
        /// <param name="birthDate">Fecha de nacimiento.</param>
        /// <param name="sinpeN">Número de Sinpe Móvil.</param>
        /// <param name="comment">Comentario.</param>
        /// <param name="status">Estado.</param>
        /// <param name="password">Contraseña.</param>
        public AffilliationForm(int cedula, string name, string lastName, string businessName, string province, string canton, string district, string address, int phoneN, DateTime birthDate, int sinpeN, string comment, string status,string password)
=======
        public AffilliationForm(int cedula, string name, string lastName, string businessName, string province, string canton, string district, string address, int phoneN, string birthDate, int sinpeN, string comment, string status,string password)
>>>>>>> camacho
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
            this.comment = comment;
            this.status = status;
            this.password = password;

        }
    }
}