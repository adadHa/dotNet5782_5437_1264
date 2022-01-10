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
        void IDal.AddStation(int id, string name, int num, double longitude, double latitude)
        {
            throw new NotImplementedException();
        }


        void IDal.AddCustomer(int id, string name, string phoneNumber, double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        void IDal.AddParcel(int customerSenderId, int customerReceiverId, string weight, string priority, int responsibleDrone)
        {
            throw new NotImplementedException();
        }

        void IDal.BindParcel(int parcelId, int droneId)
        {
            throw new NotImplementedException();
        }

        void IDal.CollectParcelByDrone(int droneId, int parcelId)
        {
            throw new NotImplementedException();
        }

        void IDal.SupplyParcelToCustomer(int parcelId)
        {
            throw new NotImplementedException();
        }

       

        void IDal.UpdateStationName(int id, string newName)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateStationChargeSlotsCap(int id, int newNum)
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

        IEnumerable<Station> IDal.ViewStationsList()
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

        IEnumerable<Parcel> IDal.ViewUnbindParcels()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Station> IDal.ViewStationsWithFreeChargeSlots()
        {
            throw new NotImplementedException();
        }

        string IDal.ViewStation(int id)
        {
            throw new NotImplementedException();
        }



        string IDal.ViewCustomer(int id)
        {
            throw new NotImplementedException();
        }

        string IDal.ViewParcel(int id)
        {
            throw new NotImplementedException();
        }

        Station IDal.GetStation(int id)
        {
            throw new NotImplementedException();
        }


        Customer IDal.GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        Parcel IDal.GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Station> IDal.GetStations(Func<Station, bool> filter)
        {
            throw new NotImplementedException();
        }



        IEnumerable<Customer> IDal.GetCustomers(Func<Customer, bool> filter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Parcel> IDal.GetParcels(Func<Parcel, bool> filter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DroneCharge> IDal.GetDroneCharges(Func<DroneCharge, bool> filter)
        {
            throw new NotImplementedException();
        }

        double[] IDal.ViewElectConsumptionData()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
