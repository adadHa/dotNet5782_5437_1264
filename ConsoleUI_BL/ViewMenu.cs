﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum ViewOptions { Exit, ViewStation, ViewDrone, ViewCustomer, ViewParcel };
        static void ViewMenu(IBL.IBL blObject)
        {
            {

                ViewOptions viewOption = 0;
                Console.WriteLine("Choose option: \n" +
            "1 - View station \n" +
            "2 - View drone \n" +
            "3 - View Ccustomer \n" +
            "4 - View parcel \n" +
            "0 - Exit");

                int x = 0;
                int.TryParse(Console.ReadLine(), out x);
                viewOption = (ViewOptions)x;

                switch (viewOption)
                {
                    case ViewOptions.ViewStation:
                        {
                            int stationIndex;
                            Console.WriteLine("Enter Station id: ");
                            int.TryParse(Console.ReadLine(), out stationIndex);

                            try
                            {
                                blObject.ViewStation(stationIndex);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                throw;
                            }
                            //Console.WriteLine(blObject.ViewStation(stationIndex).ToString());
                            break;
                        }

                    case ViewOptions.ViewDrone:
                        {
                            int droneIndex;
                            Console.WriteLine("Enter Drone id: ");
                            int.TryParse(Console.ReadLine(), out droneIndex);

                            try
                            {
                                blObject.ViewDrone(droneIndex);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                throw;
                            }
                            //Console.WriteLine(blObject.ViewDrone(droneIndex));
                            break;
                        }

                    case ViewOptions.ViewCustomer:
                        {
                            int customerIndex;
                            Console.WriteLine("Enter Customer id: ");
                            int.TryParse(Console.ReadLine(), out customerIndex);

                            try
                            {
                                blObject.ViewCustomer(customerIndex);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                throw;
                            }
                            //Console.WriteLine(blObject.ViewCustomer(customerIndex));
                            break;
                        }

                    case ViewOptions.ViewParcel:
                        {
                            int parcelIndex;
                            Console.WriteLine("Enter Parcel id: ");
                            int.TryParse(Console.ReadLine(), out parcelIndex);

                            try
                            {
                                blObject.ViewParcel(parcelIndex);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                throw;
                            }
                            //Console.WriteLine(blObject.ViewParcel(parcelIndex));
                            break;
                        }

                    case ViewOptions.Exit:
                        break;

                    default:
                        // code block
                        break;
                }


            }
        }
    }
}