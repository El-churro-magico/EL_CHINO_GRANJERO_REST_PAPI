using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REST.Models
{
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
        public DateTime birthDate { get; set; }
        public int sinpeN { get; set; }
        public string password { get; set; }

        public Producer(int cedula, string name, string lastName, string businessName,string province,string canton,string district,string address,int phoneN,DateTime birthDate,int sinpeN,string password)
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
        }
    }
}