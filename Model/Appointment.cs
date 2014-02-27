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
    public class Appointment : IAppointment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public int Frequency { get; set; }

        public double Price { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public override string ToString()
        {
            return Name + " - " + Description + " - " + StartDate.ToShortDateString() + " - " + Frequency + " - " +
                   Price + " - " + Address + " - " + ZipCode + " - " + City;
        }
    }
}
