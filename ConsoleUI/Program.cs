using System;

namespace ConsoleUI
{
    class Program
    {
        enum GeneralOptions { Exit, Add, Update, View, ListView, Sexagesimal };
        enum Add { Exit, AddStation, AddDrone, AddCustomer, AddParcel };
        enum Update { Exit, BindParcelToDrone, CollectParcelByDrone, SupplyParcelToCustomer, ChargeDrone, StopCharging };
        enum View { Exit, ViewStation, ViewDrone, ViewCustomer, ViewParcel };
        enum ListView { Exit, ViewStationsList, ViewDronesList, ViewCustomersList, ViewParcelsList, ViewUnbindParcels, ViewStationsWithFreeChargeSlots };
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
                        "4 - Add parcel \n" +
                        "0 - Exit");

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

                                        Console.Write("Enter Weight Category: ");
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

                                        dalObject.AddCustomer(id, name, phoneNumber, longitude, latitude);
                                        break;
                                    }

                                case Add.AddParcel:
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

                                        int droneId;
                                        Console.WriteLine("Enter Id of responsible drone (0 if there is no one): ");
                                        int.TryParse(Console.ReadLine(), out droneId);

                                        dalObject.AddParcel(senderId, receiverId, weight, priority, droneId);
                                        break;
                                    }

                                case Add.Exit:
                                    break;

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
                                    {
                                        int parcelId, droneId;
                                        Console.WriteLine("Enter parcel id:");
                                        int.TryParse(Console.ReadLine(), out parcelId);
                                        Console.WriteLine("Enter drone id:");
                                        int.TryParse(Console.ReadLine(), out droneId);
                                        dalObject.BindParcel(parcelId, droneId);
                                        break;
                                    }

                                case Update.CollectParcelByDrone:
                                    {
                                        int parcelId;
                                        Console.WriteLine("Enter parcel id:");
                                        int.TryParse(Console.ReadLine(), out parcelId);
                                        dalObject.CollectParcelByDrone(parcelId);
                                        break;
                                    }

                                case Update.SupplyParcelToCustomer:
                                    {
                                        int parcelId;
                                        Console.WriteLine("Enter parcel id:");
                                        int.TryParse(Console.ReadLine(), out parcelId);
                                        dalObject.SupplyParcelToCustomer(parcelId);
                                        break;
                                    }

                                case Update.ChargeDrone:
                                    {
                                        int droneId;
                                        Console.WriteLine("Enter drone id:");
                                        int.TryParse(Console.ReadLine(), out droneId);
                                        Console.WriteLine("Choose one from the following stations:");
                                        printList<IDAL.DO.Station>(dalObject.ViewStationsWithFreeChargeSlots());
                                        dalObject.ChargeDrone(droneId);
                                        break;
                                    }

                                case Update.StopCharging:
                                    {
                                        
                                        break;
                                    }

                                case Update.Exit:
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
                        "4 - View parcel \n" +
                        "0 - Exit");

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

                                case View.Exit:
                                    break;

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
                        "6 - View station with free charge slots \n" +
                        "0 - Exit");

                            int.TryParse(Console.ReadLine(), out x);
                            listViewOption = (ListView)x;

                            switch (listViewOption)
                            {
                                case ListView.ViewStationsList:
                                    {
                                        printList<IDAL.DO.Station>(dalObject.ViewStationsList());
                                        break;
                                    }

                                case ListView.ViewDronesList:
                                    {
                                        printList<IDAL.DO.Drone>(dalObject.ViewDronesList());
                                        break;
                                    }

                                case ListView.ViewCustomersList:
                                    {
                                        printList<IDAL.DO.Customer>(dalObject.ViewCustomersList());
                                        break;
                                    }

                                case ListView.ViewParcelsList:
                                    {
                                        printList<IDAL.DO.Parcel>(dalObject.ViewParcelsList());
                                        break;
                                    }

                                case ListView.ViewUnbindParcels:
                                    {
                                        
                                    }

                                case ListView.ViewStationsWithFreeChargeSlots:
                                    {
                                        printList<IDAL.DO.Station>(dalObject.ViewStationsWithFreeChargeSlots());
                                        break;
                                    }

                                case ListView.Exit:
                                    break;

                                default:
                                    // code block
                                    break;
                            }
                            break;
                        }

                    case GeneralOptions.Exit:
                        {
                            Console.WriteLine("Close Program.");
                            break;
                        }

                    case GeneralOptions.Sexagesimal:
                        {
                            double longitude;
                            Console.Write("Enter longitude: ");
                            double.TryParse(Console.ReadLine(), out longitude);

                            double latitude;
                            Console.Write("Enter latitude: ");
                            double.TryParse(Console.ReadLine(), out latitude);

                            break;
                        }

                    default:
                        // code block
                        break;
                }
            } while (option != GeneralOptions.Exit);


        }
        static private void printList<T>(T[] list)
        {
            foreach (T item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
               
