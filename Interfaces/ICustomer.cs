using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICustomer
    {
        int Id { get; set; }
        string Name { get; set; }
        string ContactPerson { get; set; }
        string Address { get; set; }
        string ZipCode { get; set; }
        string City { get; set; }
        string PhoneNumber { get; set; }
        List<IAppointment> Appointments { get; set; }
    }
}
