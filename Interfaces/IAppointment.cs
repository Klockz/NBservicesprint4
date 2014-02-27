using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{

    public interface IAppointment
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime StartDate { get; set; }
        int Frequency { get; set; }
        double Price { get; set; }
        string Address { get; set; }
        string ZipCode { get; set; }
        string City { get; set; }
    }
}
