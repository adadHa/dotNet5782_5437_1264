using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum ListView { Exit, ViewStationsList, ViewDronesList, ViewCustomersList, ViewParcelsList, ViewUnbindParcelsList, ViewStationsWithFreeChargeSlots };
        static void ListViewMenu(IBL.IBL blObject)
        {
            ListView listViewOption = 0;
            Console.WriteLine("Choose option: \n" +
        "1 - View stations list \n" +
        "2 - View drones list \n" +
        "3 - View customers list \n" +
        "4 - View parcels list \n" +
        "5 - View unbind parcels \n" +
        "6 - View station with free charge slots \n" +
        "0 - Exit");

            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            listViewOption = (ListView)x;

            switch (listViewOption)
            {
                case ListView.ViewStationsList:
                    {
                        try
                        {
                            Console.WriteLine(blObject.ViewStationsList());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case ListView.ViewDronesList:
                    {
                        try
                        {
                            Console.WriteLine(blObject.ViewDronesList());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case ListView.ViewCustomersList:
                    {
                        try
                        {
                            Console.WriteLine(blObject.ViewCustomersList());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case ListView.ViewParcelsList:
                    {
                        try
                        {
                            Console.WriteLine(blObject.ViewParcelsList());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case ListView.ViewUnbindParcelsList:
                    {
                        try
                        {
                            Console.WriteLine(blObject.ViewUnbinedParcelsList());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case ListView.ViewStationsWithFreeChargeSlots:
                    {
                        try
                        {
                            //Console.WriteLine(blObject.ViewStationsWithFreeChargeSlots());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    }

                case ListView.Exit:
                    break;

                default:
                    // code block
                    break;
            }
        }
    }
}
