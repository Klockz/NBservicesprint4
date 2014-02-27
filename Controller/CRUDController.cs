using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using DataAccess;
using Interfaces;
using Model;

namespace Controller
{
    public class CRUDController
    {
        private CustomerFacade cf;
        private SerializeController sc;
        private DataAccessFacade daf;
        public List<ICustomer> customersList;

        public CRUDController()
        {
            customersList = new List<ICustomer>();
            daf = new DataAccessFacade();
            sc = new SerializeController();
            cf = new CustomerFacade();
        }
        
        //create & save

        public ICustomer CreateCustomer()
        {
            return cf.CreateCustomer();
        }

        public void SaveCustomer(ICustomer customer)
        {
            customersList.Add(customer);
            string serializedString = sc.SerializeCustomers(customersList);
            daf.SaveCustomers(serializedString);
        }

        public List<ICustomer> SaveList(List<ICustomer> customersListfromUI)
        {
            customersList = customersListfromUI;
            string serializedString = sc.SerializeCustomers(customersList);
            daf.SaveCustomers(serializedString);

            return customersList;
        } 
        //read/load customers&appointments
        public List<ICustomer> LoadCustomers()
        {
            string stringToDeserialize = daf.LoadCustomers();
            customersList = sc.DeserializeCustomers(stringToDeserialize);
            if (customersList != null)
            {
                return customersList;
            }
            else
            {

                return new List<ICustomer>();  
            }
        } 

        //update customers&appointments
        public void UpdateCustomer(ICustomer custToUpdate)
        {
            
        }

        //delete customers&appointments

    }
}
