using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum ListView { Exit, ViewStationsList, ViewDronesList, ViewCustomersList, ViewParcelsList, ViewUnbindParcels, ViewStationsWithFreeChargeSlots };
        static void ListViewMenu(IBL.IBL blObject)
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

            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            listViewOption = (ListView)x;

            switch (listViewOption)
            {
                case ListView.ViewStationsList:
                    {
                        try
                        {
                            printList<IDAL.DO.Station>(
                               (System.Collections.Generic.List<IDAL.DO.Station>)blObject.ViewStationsList());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
                        }
                        break;
                    }

                case ListView.ViewDronesList:
                    {
                        try
                        {
                            printList<IDAL.DO.Drone>(
                                (System.Collections.Generic.List<IDAL.DO.Drone>)blObject.ViewDronesList());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
                        }
                        break;
                    }

                case ListView.ViewCustomersList:
                    {
                        try
                        {
                            printList<IDAL.DO.Customer>(
                                (System.Collections.Generic.List<IDAL.DO.Customer>)blObject.ViewCustomersList());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
                        }
                        break;
                    }

                case ListView.ViewParcelsList:
                    {
                        try
                        {
                            printList<IDAL.DO.Parcel>(
                                (System.Collections.Generic.List<IDAL.DO.Parcel>)blObject.ViewParcelsList());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
                        }
                        break;
                    }

                case ListView.ViewUnbindParcels:
                    {
                        try
                        {
                            printList<IDAL.DO.Parcel>(
                                (System.Collections.Generic.List<IDAL.DO.Parcel>)blObject.ViewUnbindParcels());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
                        }
                        break;
                    }

                case ListView.ViewStationsWithFreeChargeSlots:
                    {
                        try
                        {
                            printList<IDAL.DO.Station>(
                                (System.Collections.Generic.List<IDAL.DO.Station>)blObject.ViewStationsWithFreeChargeSlots());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw;
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
