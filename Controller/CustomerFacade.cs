using DataAccess;
using Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class CustomerFacade
    {
        public List<ICustomer> customersList { get; set; }

        public CustomerFacade()
        {
            customersList = new List<ICustomer>();
        }

        public ICustomer CreateCustomer()
        {
            return new Customer();
        }

        public void SaveCustomer(ICustomer customer)
        {
            customersList.Add(customer);
            CustomerAccessFacade.SaveAllCustomers(customersList);
        }

        public List<ICustomer> LoadAllCustomers()
        {
            customersList = CustomerAccessFacade.LoadAllCustomers();
            return customersList;
        }

        public void AddAppointmentToCustomer(IAppointment iappointment, ICustomer icustomer)
        {
           
        }

        public List<IAppointment> AddAllCustomerAppointmentsToList(ICustomer icustomer, List<IAppointment> iappointmentsList)
        {
            for (int i = 0; i < icustomer.Appointments.Count; i++)
            {
                iappointmentsList.Add(icustomer.Appointments[i]);
            }
            return iappointmentsList;
        }

        public bool Update(ICustomer customer)
        {
            return CustomerAccessFacade.Update(customer);
        }

        public bool Delete(ICustomer customer)
        {
            return CustomerAccessFacade.Delete(customer);
        }
    }
}
