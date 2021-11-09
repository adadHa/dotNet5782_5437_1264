using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    partial class Program
    {
        enum UpdateOptions { Exit, UpdateDrone, UpdateStation, UpdateCustomer, SendDroneToCharge, ReleaseDroneFromCharge, BindParcelToDrone, CollectParcelByDrone, DeliverParcelByDrone };
        static void UpdateMenu(IBL.IBL blObject)
        {
            UpdateOptions updateOption = 0;
            Console.WriteLine("Choose option: \n" +
                            "1 - Update Drone \n" +
                            "2 - Update Station \n" +
                            "3 - Update Customer \n" +
                            "3 - Send Drone To Charge \n" +
                            "4 - Release Drone From Charge \n" +
                            "5 - Bind Parcel To Drone \n" +
                            "6 - Collect Parcel By Drone \n" +
                            "7 - Deliver Parcel By Drone");
            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            updateOption = (UpdateOptions)x;

            switch (updateOption)
            {
                case UpdateOptions.UpdateDrone:
                    {
                        int droneId;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        blObject.UpdateDrone(droneId, name);
                        break;
                    }

                case UpdateOptions.UpdateStation:
                    {
                        int id;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.Write("Enter name of station: ");
                        string name = Console.ReadLine();

                        blObject.UpdateStation(id, name);
                        break;
                    }

                case UpdateOptions.UpdateCustomer:
                    {
                        int id;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.Write("Enter name of station: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Phone Number: ");
                        string phoneNumber = Console.ReadLine();

                        blObject.UpdateCustomer(id, name, phoneNumber);
                        break;
                    }

                case UpdateOptions.SendDroneToCharge:
                    {
                        int droneId, stationId;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);
                        
                        break;
                    }

                case UpdateOptions.ReleaseDroneFromCharge:
                    {
                        int droneId;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);
                        
                        break;
                    }

                case UpdateOptions.BindParcelToDrone:
                    {
                        int parcelId, droneId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        blObject.BindParcelToDrone(droneId);
                        break;
                    }

                

                case UpdateOptions.CollectParcelByDrone:
                    {
                        int droneId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        blObject.CollectParcelByDrone(droneId);
                        break;
                    }

                case UpdateOptions.DeliverParcelByDrone:
                    {
                        int droneId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        blObject.SupplyParcelToCustomer(droneId);
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
