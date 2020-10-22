using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
    /// <summary>
    /// Clase para representar un token.
    /// </summary>
    public class Token
    {
        public string token { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="token">Token.</param>
        public Token(string token)
        {
            this.token = token;
        }
    }
}