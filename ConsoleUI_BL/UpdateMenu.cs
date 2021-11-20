using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum UpdateOptions { Exit, UpdateDrone, UpdateStation, UpdateCustomer, ChargeDrone, ReleaseDroneFromCharge, BindParcelToDrone, CollectParcelByDrone, DeliverParcelByDrone };
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

                        Console.Write("Enter new model: ");
                        string model = Console.ReadLine();

                        try
                        {
                            blObject.UpdateDrone(droneId, model);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case UpdateOptions.UpdateStation:
                    {
                        int id;
                        Console.WriteLine("Enter station id:");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.WriteLine("Enter the values you want to update (if no, just press enter): ");
                        Console.Write("A new name for the station - ");
                        string newName = Console.ReadLine();

                        int newNum; string input;
                        Console.Write("A new chargeslots capcity - ");
                        input = Console.ReadLine();
                        if (input != "")
                            int.TryParse(input, out newNum);
                        else
                            newNum = -1;

                        try
                        {
                            blObject.UpdateStation(id, newName, newNum);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case UpdateOptions.UpdateCustomer:
                    {
                        int id;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.WriteLine("Enter the values you want to update (if no, just press enter): ");
                        Console.Write("Enter name of station - ");
                        string newName = Console.ReadLine();

                        Console.Write("Enter Phone Number - ");
                        string newPhoneNumber = Console.ReadLine();

                        try
                        {
                            blObject.UpdateCustomer(id, newName, newPhoneNumber);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case UpdateOptions.ChargeDrone:
                    {
                        int droneId;
                        Console.WriteLine("Enter drone id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        try
                        {
                            blObject.ChargeDrone(droneId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
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

                        //blObject.BindParcelToDrone(droneId);
                        break;
                    }



                case UpdateOptions.CollectParcelByDrone:
                    {
                        int droneId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        //blObject.CollectParcelByDrone(droneId);
                        break;
                    }

                case UpdateOptions.DeliverParcelByDrone:
                    {
                        int droneId;
                        Console.WriteLine("Enter parcel id:");
                        int.TryParse(Console.ReadLine(), out droneId);

                        //blObject.SupplyParcelToCustomer(droneId);
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
}
