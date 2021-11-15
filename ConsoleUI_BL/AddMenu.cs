using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum AddOptions { Exit, AddStation, AddDrone, AddCustomer, AddParcel };
        static void AddMenu(IBL.IBL blObject)
        {
            AddOptions addOption = 0;
            Console.WriteLine("Choose add option: \n" +
                        "1 - Add station \n" +
                        "2 - Add drone \n" +
                        "3 - Add customer \n" +
                        "4 - Add parcel \n" +
                        "0 - Exit");

            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            addOption = (AddOptions)x;

            switch (addOption)
            {
                case AddOptions.AddStation:
                    {

                        int id;
                        Console.WriteLine("Enter Id: ");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        int num;
                        Console.WriteLine("Enter number of free charge station: ");
                        int.TryParse(Console.ReadLine(), out num);

                        double longitude;
                        Console.Write("Enter location- longitude: ");
                        double.TryParse(Console.ReadLine(), out longitude);

                        double latitude;
                        Console.Write("latitude: ");
                        double.TryParse(Console.ReadLine(), out latitude);

                        IBL.BO.Location location = new IBL.BO.Location() { Longitude = longitude, Latitude = latitude };

                        try
                        {
                            blObject.AddStation(id, name, num, location);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case AddOptions.AddDrone:
                    {
                        int id;
                        Console.WriteLine("Enter Id: ");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.Write("Enter Weight Category: ");
                        string weight = Console.ReadLine();

                        int batteryStatus;
                        Console.WriteLine("Enter Battery status: ");
                        int.TryParse(Console.ReadLine(), out batteryStatus);

                        Console.Write("Enter Drone Status: ");
                        string droneStatus = Console.ReadLine();

                        try
                        {
                            //blObject.AddDrone(id, weight, batteryStatus, droneStatus);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case AddOptions.AddCustomer:
                    {
                        int id;
                        Console.WriteLine("Enter Id: ");
                        int.TryParse(Console.ReadLine(), out id);

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Phone Number: ");
                        string phoneNumber = Console.ReadLine();

                        try
                        {
                            blObject.AddCustomer(id, name, phoneNumber);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case AddOptions.AddParcel:
                    {

                        int senderId;
                        Console.WriteLine("Enter customer sender Id: ");
                        int.TryParse(Console.ReadLine(), out senderId);

                        int receiverId;
                        Console.WriteLine("Enter customer receiver Id: ");
                        int.TryParse(Console.ReadLine(), out receiverId);

                        Console.Write("Enter Weight Catagory: ");
                        string weight = Console.ReadLine();

                        Console.Write("Enter Priority: ");
                        string priority = Console.ReadLine();

                        try
                        {
                            blObject.AddParcel(senderId, receiverId, weight, priority);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case AddOptions.Exit:
                    break;

                default:
                    // code block
                    break;
            }
        }

    }
}
