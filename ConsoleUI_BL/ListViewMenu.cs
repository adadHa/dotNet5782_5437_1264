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
                        //printList<IDAL.DO.Station>(
                        //    (System.Collections.Generic.List<IDAL.DO.Station>)blObject.ViewStationsList());
                        break;
                    }

                case ListView.ViewDronesList:
                    {
                        //printList<IDAL.DO.Drone>(
                        //    (System.Collections.Generic.List<IDAL.DO.Drone>)blObject.ViewDronesList());
                        break;
                    }

                case ListView.ViewCustomersList:
                    {
                        //printList<IDAL.DO.Customer>(
                        //    (System.Collections.Generic.List<IDAL.DO.Customer>)blObject.ViewCustomersList());
                        break;
                    }

                case ListView.ViewParcelsList:
                    {
                        //printList<IDAL.DO.Parcel>(
                        //    (System.Collections.Generic.List<IDAL.DO.Parcel>)blObject.ViewParcelsList());
                        break;
                    }

                case ListView.ViewUnbindParcels:
                    {
                        //printList<IDAL.DO.Parcel>(
                        //    (System.Collections.Generic.List<IDAL.DO.Parcel>)blObject.ViewUnbindParcels());
                        break;
                    }

                case ListView.ViewStationsWithFreeChargeSlots:
                    {
                        //printList<IDAL.DO.Station>(
                        //    (System.Collections.Generic.List<IDAL.DO.Station>)blObject.ViewStationsWithFreeChargeSlots());
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
