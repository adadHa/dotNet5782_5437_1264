using System;

namespace ConsoleUI
{
    class Program
    {
        enum GeneralOptions { Exit, Add, Update, View, ListView };
        enum Add { AddStation, AddDrone, AddCustomer, AddParcel };
        enum Update { BindParcel, CollectParcelByDrone, SupplyParcelToCustomer, SendDroneToCharge, ReleaseDroneFromCharge };
        enum View { ViewStation, ViewDrone, ViewCustomer, ViewParcel };
        enum ListView { ViewStationList, ViewDronesList, ViewCustomersList, ViewParcelsList, ViewUnbindParcel, ViewFreeChargeStations };
        static void Main(string[] args)
        {
            GeneralOptions option = 0;
            Console.WriteLine("Choose Option: /n" +
                "1 - Add /n" +
                "2 - Update /n" +
                "3 - View /n" +
                "4 - ListView /n");
            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            option = (GeneralOptions)x;

            switch (option)
            {

                case GeneralOptions.Add:
                    {
                        Add addOption = 0;
                        Console.WriteLine("Choose Option: /n" +
                    "1 - AddStation /n" +
                    "2 - AddDrone /n" +
                    "3 - AddCustomer /n" +
                    "4 - AddParcel /n");

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

                                    break;
                                }

                            case Add.AddCustomer:
                                {
                                    int id;
                                    Console.WriteLine("Enter Id: ");
                                    int.TryParse(Console.ReadLine(), out id);

                                    Console.Write("Enter Name: ");
                                    string name = Console.ReadLine();

                                    int phoneNumber;
                                    Console.WriteLine("Enter Phone Number: ");
                                    int.TryParse(Console.ReadLine(), out phoneNumber);

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
                                    int id;
                                    Console.WriteLine("Enter Id: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    
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
                        Console.WriteLine("Choose Option: /n" +
                    "1 - BindParcel /n" +
                    "2 - CollectParcelByDrone /n" +
                    "3 - SupplyParcelToCustomer /n" +
                    "4 - SendDroneToCharge /n" +
                    "5 - ReleaseDroneFromCharge /n");

                        int.TryParse(Console.ReadLine(), out x);
                        updateOption = (Update)x;

                        switch (updateOption)
                        {
                            case Update.BindParcel:
                                // code block
                                break;

                            case Update.CollectParcelByDrone:
                                // code block
                                break;

                            case Update.SupplyParcelToCustomer:
                                // code block
                                break;

                            case Update.SendDroneToCharge:
                                // code block
                                break;

                            case Update.ReleaseDroneFromCharge:
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
                        Console.WriteLine("Choose Option: /n" +
                    "1 - ViewStation /n" +
                    "2 - ViewDrone /n" +
                    "3 - ViewCustomer /n" +
                    "4 - ViewParcel /n");

                        int.TryParse(Console.ReadLine(), out x);
                        viewOption = (View)x;

                        switch (viewOption)
                        {
                            case View.ViewStation:
                                // code block
                                break;

                            case View.ViewDrone:
                                // code block
                                break;

                            case View.ViewCustomer:
                                // code block
                                break;

                            case View.ViewParcel:
                                // code block
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
                        Console.WriteLine("Choose Option: /n" +
                    "1 - ViewStationList /n" +
                    "2 - ViewDronesList /n" +
                    "3 - ViewCustomersList /n" +
                    "4 - ViewParcelsList /n" +
                    "5 - ViewUnbindParcel /n" +
                    "6 - ViewFreeChargeStations /n");

                        int.TryParse(Console.ReadLine(), out x);
                        listViewOption = (ListView)x;

                        switch (listViewOption)
                        {
                            case ListView.ViewStationList:
                                // code block
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

                            case ListView.ViewUnbindParcel:
                                // code block
                                break;

                            case ListView.ViewFreeChargeStations:
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
        }
    }
}