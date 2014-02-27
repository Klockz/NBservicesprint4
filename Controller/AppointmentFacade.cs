using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Interfaces;

namespace Controller
{
    public class AppointmentFacade
    {
        public List<IAppointment> appointmentsList { get; set; }

        public AppointmentFacade(CustomerFacade customerfacade)
        {
            foreach (ICustomer icustomer in customerfacade.customersList)
            {
                customerfacade.AddAllCustomerAppointmentsToList(icustomer, appointmentsList);
            }
        }

        public static IAppointment CreateAppointment()
        {
            return new Appointment();
        }
    }
}
