using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar a un cliente.
    /// </summary>
    public class Client
    {
        public int cedula { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string province { get; set; }
        public string canton { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public int phoneN { get; set; }
        public string birthDate { get; set; }
        public string userName { get; set; }
        private string password { get; set; }
        public ArrayList notifications;
<<<<<<< HEAD

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="cedula">Cédula.</param>
        /// <param name="name">Nombre.</param>
        /// <param name="lastName">Apellidos.</param>
        /// <param name="province">Provincia.</param>
        /// <param name="canton">Cantón.</param>
        /// <param name="district">Distrito.</param>
        /// <param name="address">Dirección.</param>
        /// <param name="phoneN">Teléfono.</param>
        /// <param name="birthDate">Fecha de nacimiento.</param>
        /// <param name="userName">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        public Client(int cedula,string name, string lastName, string province,string canton,string district,string address,int phoneN,string birthDate, string userName,string password)
=======
        public int compras { get; set; }
        public Client(int cedula,string name, string lastName, string province,string canton,string district,string address,int phoneN,string birthDate, string userName,string password,int compras)
>>>>>>> camacho
        {
            this.cedula = cedula;
            this.name = name;
            this.lastName = lastName;
            this.province = province;
            this.canton = canton;
            this.district = district;
            this.address = address;
            this.phoneN = phoneN;
            this.birthDate = birthDate;
            this.userName = userName;
            this.password = password;
            this.compras = compras;

        }

        /// <summary>
        /// Método para obtener la contraseña de un cliente.
        /// </summary>
        /// <returns>Retorna la contraseña de un cliente.</returns>
        public string getPassword()
        {
            return this.password;
        }

        /// <summary>
        /// Método para añadir notificaciones asociadas a un cliente.
        /// </summary>
        /// <param name="notifications">Lista de notificaciones.</param>
        public void addNotifications(ArrayList notifications)
        {
            this.notifications = notifications;
        }
    }
}