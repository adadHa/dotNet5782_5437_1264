using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace DalXml
{
    public sealed partial class DalXml : IDal
    {
        void IDal.AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            XElement cutomersRootElement = XMLTools.LoadListFromXMLElement(CustomersPath);
            XElement customer = (from c in cutomersRootElement.Elements()
                                where (int.Parse(c.Element("id").Value) == id)
                                select c).FirstOrDefault();

            if (customer != null)
                throw new IdIsAlreadyExistException(id, "Customer");

            else
            {
                XElement newCustomer = new XElement("Customer", 
                                            new XElement("Id" , id.ToString()),
                                            new XElement("Name", name.ToString()),
                                            new XElement("PhoneNumber", phoneNumber.ToString()),
                                            new XElement("Longitude", longitude.ToString()),
                                            new XElement("Latitude", latitude.ToString()));
                cutomersRootElement.Add(newCustomer);
                XMLTools.SaveListToXMLElement(cutomersRootElement, CustomersPath);
            }   
        }
        void IDal.UpdateCustomerName(int id, string newName)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateCustomrePhoneNumber(int id, string newPhoneNumber)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Customer> IDal.ViewCustomersList()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Parcel> IDal.ViewParcelsList()
        {
            throw new NotImplementedException();
        }

        string IDal.ViewCustomer(int id)
        {
            throw new NotImplementedException();
        }

        Customer IDal.GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Customer> IDal.GetCustomers(Func<Customer, bool> filter)
        {
            throw new NotImplementedException();
        }
    }

}
