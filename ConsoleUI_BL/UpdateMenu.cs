using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    partial class Program
    {
        enum UpdateOptions { Exit, AddStation, AddDrone, AddCustomer, AddParcel };
        static void UpdateMenu(IBL.IBL blObject)
        {
            UpdateOptions updateOption = 0;
            Console.WriteLine("Choose option: \n" +
                            "1 - Bind parcel to a drone \n" +
                            "2 - Collect parcel by drone \n" +
                            "3 - Supply parcel to customer \n" +
                            "4 - Charge drone \n" +
                            "5 - Stop drone's charging \n");
            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            updateOption = (UpdateOptions)x;

            switch (updateOption)
            {
                case UpdateOptions.BindParcelToDrone:
                    {
                        int parcelId, droneId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out parcelId);
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);
                        dalObject.BindParcel(parcelId, droneId);
                        break;
                    }

                case UpdateOptions.CollectParcelByDrone:
                    {
                        int parcelId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out parcelId);
                        dalObject.CollectParcelByDrone(parcelId);
                        break;
                    }

                case UpdateOptions.SupplyParcelToCustomer:
                    {
                        int parcelId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out parcelId);
                        dalObject.SupplyParcelToCustomer(parcelId);
                        break;
                    }

                case UpdateOptions.ChargeDrone:
                    {
                        int droneId, stationId;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);
                        Console.WriteLine("Choose one from the following stations:");
                        printList<IDAL.DO.Station>(
                            (System.Collections.Generic.List<IDAL.DO.Station>)dalObject.ViewStationsWithFreeChargeSlots());
                        int.TryParse(Console.ReadLine(), out stationId);
                        dalObject.ChargeDrone(droneId);
                        break;
                    }

                case UpdateOptions.StopCharging:
                    {
                        int droneId;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);
                        dalObject.StopCharging(droneId);
                        break;
                    }

                case UpdateOptions.Exit:
                    break;

                default:
                    // code block
                    break;
            }
        }
    }
