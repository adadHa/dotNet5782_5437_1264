using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DalXml
{
    public sealed partial class DalXml :IDal
    {
        #region Singelton
        //implement Lazy Instantiation (and thread-safe) singleton design pattern
        //     Lazy - create the BL entity only when called
        //     thread safe - the Nested class loader will do all static initialization before
        //                  it will enable other threads accessing the class
        public DalXml()
        {
            //DataSource.Initialize();
        }

        public static DalXml Instance { get { return Nested.Instance; } }
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly DalXml Instance = new DalXml();
        }
        #endregion
        #region datasource xml file's pathes
        string DronesPath = @"DronesXML.xml";
        string DronesChargesPath = @"DronesCharges.xml";
        string StationsPath = @"Stations.xml";
        string ParcelsPath = @"Parcels.xml";
        #endregion
        

        void IDal.AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            throw new NotImplementedException();
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



        double[] IDal.ViewElectConsumptionData()
        {
            throw new NotImplementedException();
        }

        public string ViewParcel(int id)
        {
            throw new NotImplementedException();
        }
    }

}
