using System.Xml.Serialization;
using Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Customer : ICustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public List<IAppointment> Appointments { get; set; }


        public override string ToString()
        {
            return Name + " - " + ContactPerson + " - " + Address + " - " + ZipCode + " - " + City + " - " + PhoneNumber;
        }
    }
}
