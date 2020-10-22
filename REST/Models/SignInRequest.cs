using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{   
    /// <summary>
    /// Clase para representar solicitud de inicio de sesión.
    /// </summary>
    public class SignInRequest
    {
  
        public string password { get; set; }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="password">Contraseña.</param>
        public SignInRequest(string password)
        {
            this.password = password;
        }
    }
}