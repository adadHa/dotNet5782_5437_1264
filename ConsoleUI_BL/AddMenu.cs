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
        static void AddMenu(BlApi.IBL blObject)
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

                        BO.Location location = new BO.Location() { Longitude = longitude, Latitude = latitude };

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

                        Console.WriteLine("Enter one of the following weight categories: ");
                        foreach (BO.WheightCategories a in Enum.GetValues(typeof(BO.WheightCategories)))
                        {
                            Console.WriteLine(a.ToString());
                        }
                        string weight = Console.ReadLine();

                        Console.Write("Enter model: ");
                        string model = Console.ReadLine();

                        int initialStationId;
                        Console.WriteLine("Enter station id for intial charging: ");
                        int.TryParse(Console.ReadLine(), out initialStationId);

                        try
                        {
                            blObject.AddDrone(id, model, weight, initialStationId);
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
                            //blObject.AddCustomer(id, name, phoneNumber);

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

                        Console.WriteLine("Enter one of the following weight categories: ");
                        foreach (BO.WheightCategories a in Enum.GetValues(typeof(BO.WheightCategories)))
                        {
                            Console.WriteLine(a.ToString());
                        }
                        string weight = Console.ReadLine();

                        Console.Write("Enter one of the following priorities: ");
                        foreach (BO.Priorities a in Enum.GetValues(typeof(BO.Priorities)))
                        {
                            Console.WriteLine(a.ToString());
                        }
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
