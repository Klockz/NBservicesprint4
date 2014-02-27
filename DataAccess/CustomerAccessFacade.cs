using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class CustomerAccessFacade
    {
        public static List<ICustomer> LoadAllCustomers()
        {
            List<ICustomer> customers = new List<ICustomer>();

            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    return bin.Deserialize(stream) as List<ICustomer>;
                }
            }
            catch (Exception)
            {
            }
            return customers;
        }

        public static void SaveAllCustomers(List<ICustomer> customersList)
        {
            using (Stream stream = File.Open("data.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, customersList);
            }
        }

        public static bool Update(ICustomer customer)
        {
            List<ICustomer> customers = LoadAllCustomers();
            ICustomer customerToBeUpdated = null;
            for (int i = 0; i < customers.Count; i++)
            {
                ICustomer customerLoaded = customers[i];
                if (customer.Id == customerLoaded.Id)
                {
                    customerToBeUpdated = customerLoaded;
                    break;
                }
            }
            if (customerToBeUpdated != null)
            {
                customers.Remove(customerToBeUpdated);
                customers.Add(customer);
                SaveAllCustomers(customers);
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool Delete(ICustomer customer)
        {
            ICustomer customerToBeDeleted = null;
            List<ICustomer> customers = LoadAllCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                ICustomer customerLoaded = customers[i];
                if (customer.Id == customerLoaded.Id)
                {
                    customerToBeDeleted = customerLoaded;
                    break;
                }
            }
            if (customerToBeDeleted != null)
            {
                customers.Remove(customerToBeDeleted);
                SaveAllCustomers(customers);
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
