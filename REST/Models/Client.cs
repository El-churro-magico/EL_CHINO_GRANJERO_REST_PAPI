using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
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
        public DateTime birthDate { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        
        public Client(int cedula, string name, string lastName, string province, string canton, string district, string address, int phoneN, DateTime birthDate, string user, string password)
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
            this.user = user;
            this.password = password;
        }
    }
}