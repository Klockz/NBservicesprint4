using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace DataAccess
{
    public class DataAccessFacade
    {
        public string LoadCustomers()
        {
            string JSONString = "";
            try
            {
                StreamReader sr = new StreamReader(@"C:/file.txt");

                while (sr.EndOfStream != true)
                {
                    JSONString += sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return JSONString;
        }

        public void SaveCustomers(string serializedString)
        {
                
                StreamWriter sw = new StreamWriter(@"file.txt");
                sw.WriteLine(serializedString);
                sw.Close();
           
          
        }
    }
}
