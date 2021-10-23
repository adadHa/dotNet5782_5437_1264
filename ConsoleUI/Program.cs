using System;

namespace ConsoleUI
{
    class Program
    {
        enum GeneralOptions { Exit, Add, Update, View, ListView };
        enum Add { AddStation, AddDrone, AddCustomer, AddParcel };
        enum Update { BindParcelToDrone, CollectParcelByDrone, SupplyParcelToCustomer, ChargeDrone, StopCharging };
        enum View { ViewStation, ViewDrone, ViewCustomer, ViewParcel };
        enum ListView { ViewStationsList, ViewDronesList, ViewCustomersList, ViewParcelsList, ViewUnbindParcels, ViewStationsWithFreeChargeSlots };
        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new DalObject.DalObject();
            GeneralOptions option = 0;
            do
            {
                Console.WriteLine("Choose Option: \n" +
                "1 - Add \n" +
                "2 - Update \n" +
                "3 - View \n" +
                "4 - View lists \n" +
                "0 - Exit");
                int x = 0;
                int.TryParse(Console.ReadLine(), out x);
                option = (GeneralOptions)x;

                switch (option)
                {

                    case GeneralOptions.Add:
                        {
                            Add addOption = 0;
                            Console.WriteLine("Choose add option: \n" +
                        "1 - Add station \n" +
                        "2 - Add drone \n" +
                        "3 - Add customer \n" +
                        "4 - Add parcel \n");

                            int.TryParse(Console.ReadLine(), out x);
                            addOption = (Add)x;

                            switch (addOption)
                            {
                                case Add.AddStation:
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
                                        Console.Write("Enter longitude: ");
                                        double.TryParse(Console.ReadLine(), out longitude);

                                        double latitude;
                                        Console.Write("Enter latitude: ");
                                        double.TryParse(Console.ReadLine(), out latitude);

                                        dalObject.AddStation(id, name, num, longitude, latitude);
                                        break;
                                    }

                                case Add.AddDrone:
                                    {
                                        int id;
                                        Console.WriteLine("Enter Id: ");
                                        int.TryParse(Console.ReadLine(), out id);

                                        Console.Write("Enter Model: ");
                                        string model = Console.ReadLine();

                                        Console.Write("Enter Weight Catagory: ");
                                        string weight = Console.ReadLine();

                                        int batteryStatus;
                                        Console.WriteLine("Enter Battery status: ");
                                        int.TryParse(Console.ReadLine(), out batteryStatus);

                                        Console.Write("Enter Drone Status: ");
                                        string droneStatus = Console.ReadLine();

                                        dalObject.AddDrone(id, model, weight, batteryStatus, droneStatus);
                                        break;
                                    }

                                case Add.AddCustomer:
                                    {
                                        int id;
                                        Console.WriteLine("Enter Id: ");
                                        int.TryParse(Console.ReadLine(), out id);

                                        Console.Write("Enter Name: ");
                                        string name = Console.ReadLine();

                                        Console.Write("Enter Phone Number: ");
                                        string phoneNumber = Console.ReadLine();

                                        double longitude;
                                        Console.Write("Enter longitude: ");
                                        double.TryParse(Console.ReadLine(), out longitude);

                                        double latitude;
                                        Console.Write("Enter latitude: ");
                                        double.TryParse(Console.ReadLine(), out latitude);

                                        break;
                                    }

                                case Add.AddParcel:
                                    {

                                        int customerSenderId;
                                        Console.WriteLine("Enter customer sender Id: ");
                                        int.TryParse(Console.ReadLine(), out customerSenderId);

                                        int customerReceiverId;
                                        Console.WriteLine("Enter customer receiver Id: ");
                                        int.TryParse(Console.ReadLine(), out customerReceiverId);

                                        Console.Write("Enter Weight Catagory: ");
                                        string weight = Console.ReadLine();

                                        Console.Write("Enter Priority: ");
                                        string priority = Console.ReadLine();

                                        int responsibleDrone;
                                        Console.WriteLine("Enter Id of responsible drone (0 if there is no one): ");
                                        int.TryParse(Console.ReadLine(), out responsibleDrone);

                                        break;
                                    }
                                default:
                                    // code block
                                    break;
                            }
                            break;
                        }

                    case GeneralOptions.Update:
                        {
                            Update updateOption = 0;
                            Console.WriteLine("Choose option: \n" +
                        "1 - Bind parcel to a drone \n" +
                        "2 - Collect parcel by drone \n" +
                        "3 - Supply parcel to customer \n" +
                        "4 - Charge drone \n" +
                        "5 - Stop drone's charging \n");

                            int.TryParse(Console.ReadLine(), out x);
                            updateOption = (Update)x;

                            switch (updateOption)
                            {
                                case Update.BindParcelToDrone:
                                    // code block
                                    break;

                                case Update.CollectParcelByDrone:
                                    // code block
                                    break;

                                case Update.SupplyParcelToCustomer:
                                    // code block
                                    break;

                                case Update.ChargeDrone:
                                    // code block
                                    break;

                                case Update.StopCharging:
                                    // code block
                                    break;

                                default:
                                    // code block
                                    break;
                            }
                            break;
                        }


                    case GeneralOptions.View:
                        {
                            View viewOption = 0;
                            Console.WriteLine("Choose option: \n" +
                        "1 - View station \n" +
                        "2 - View drone \n" +
                        "3 - View Ccustomer \n" +
                        "4 - View parcel \n");

                            int.TryParse(Console.ReadLine(), out x);
                            viewOption = (View)x;

                            switch (viewOption)
                            {
                                case View.ViewStation:
                                    {
                                        int stationIndex;
                                        Console.WriteLine("Enter Station id: ");
                                        int.TryParse(Console.ReadLine(), out stationIndex);
                                        Console.WriteLine(dalObject.ViewStation(stationIndex).ToString());
                                        break;
                                    }

                                case View.ViewDrone:
                                    {
                                        int droneIndex;
                                        Console.WriteLine("Enter Drone id: ");
                                        int.TryParse(Console.ReadLine(), out droneIndex);
                                        Console.WriteLine(dalObject.ViewDrone(droneIndex));
                                        break;
                                    }

                                case View.ViewCustomer:
                                    {
                                        int customerIndex;
                                        Console.WriteLine("Enter Customer id: ");
                                        int.TryParse(Console.ReadLine(), out customerIndex);
                                        Console.WriteLine(dalObject.ViewCustomer(customerIndex));
                                        break;
                                    }

                                case View.ViewParcel:
                                    {
                                        int parcelIndex;
                                        Console.WriteLine("Enter Parcel id: ");
                                        int.TryParse(Console.ReadLine(), out parcelIndex);
                                        Console.WriteLine(dalObject.ViewParcel(parcelIndex));
                                        break;
                                    }

                                default:
                                    // code block
                                    break;
                            }
                            break;
                        }

                    case GeneralOptions.ListView:
                        {
                            ListView listViewOption = 0;
                            Console.WriteLine("Choose option: \n" +
                        "1 - View stations list \n" +
                        "2 - View drones list \n" +
                        "3 - View costomers list \n" +
                        "4 - View parcels list \n" +
                        "5 - View unbind parcels \n" +
                        "6 - View station with free charge slots \n");

                            int.TryParse(Console.ReadLine(), out x);
                            listViewOption = (ListView)x;

                            switch (listViewOption)
                            {
                                case ListView.ViewStationsList:

                                    break;

                                case ListView.ViewDronesList:
                                    // code block
                                    break;

                                case ListView.ViewCustomersList:
                                    // code block
                                    break;

                                case ListView.ViewParcelsList:
                                    // code block
                                    break;

                                case ListView.ViewUnbindParcels:
                                    // code block
                                    break;

                                case ListView.ViewStationsWithFreeChargeSlots:
                                    // code block
                                    break;

                                default:
                                    // code block
                                    break;
                            }
                            break;
                        }

                    case GeneralOptions.Exit:
                        Console.WriteLine("Close Program.");
                        break;

                    default:
                        // code block
                        break;
                }
            } while (option != GeneralOptions.Exit);

            
        }
    }
}