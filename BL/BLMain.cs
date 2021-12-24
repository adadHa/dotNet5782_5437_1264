using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    internal sealed partial class BL : BlApi.IBL
    {
        //implement Lazy Instantiation (and thread-safe) singleton design pattern
        // Lazy - create the BL entity only when called
        // thread safe - the Nested class loader will do all static initialization before
        //               it will enable other threads accessing the class
        private BL()
        {
            dalObject = DalApi.DalFactory.GetDal("DalObject");
            double[] arr = dalObject.ViewElectConsumptionData();
            availableDrElectConsumption = arr[0];
            lightDrElectConsumption = arr[1];
            mediumDrElectConsumption = arr[2];
            heavyDrElectConsumption = arr[3];
            chargingRate = arr[4];
            InitializeDrones();
        }
        public static BL Instance { get { return Nested.Instance; } }
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly BL Instance = new BL();
        }
        private DalApi.IDal dalObject;
        private List<BO.DroneForList> BLDrones = new List<BO.DroneForList>();
        private double availableDrElectConsumption;
        private double lightDrElectConsumption;
        private double mediumDrElectConsumption;
        private double heavyDrElectConsumption;
        private double chargingRate;
        static Random rand = new Random();

    }
}

