using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Interfaces;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Controller
{
    public class SerializeController
    {
        internal string SerializeCustomers(List<ICustomer> customers)
        {
            string serializedString = JsonConvert.SerializeObject(customers);
            return serializedString;
        }

        internal List<ICustomer> DeserializeCustomers(string stringToDeserialize)
        {
            List<ICustomer> customers = JsonConvert.DeserializeObject<List<ICustomer>>(stringToDeserialize);
            return customers;
        }
    }
}
