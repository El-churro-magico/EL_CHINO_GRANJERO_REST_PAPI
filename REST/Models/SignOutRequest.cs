using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar solicitud de cierre de sesión.
    /// </summary>
    public class SignOutRequest
    {
        public string type { get; set; }
        public string token { get; set; }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="type">Tipo de usuario.</param>
        /// <param name="token">Token.</param>
        public SignOutRequest(string type,string token)
        {
            this.type = type;
            this.token = token;
        }
    }
}