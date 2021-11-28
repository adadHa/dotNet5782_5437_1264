using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerForList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int CounterSendAndSupplied { get; set; }
            public int CounterSendAndNotSupplied { get; set; }
            public int CounterRecievedParcels { get; set; }
            public int CounterParcelsOnMyWay { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Phone: {Phone}\n" +
                    $"      Number Of Sent And Supplied Parcels: {CounterSendAndSupplied}\n" +
                    $"      Number Of Parcels which sent but not supplied: {CounterSendAndNotSupplied}\n" +
                    $"      Number Of recieved parcels:{CounterRecievedParcels}\n" +
                    $"      Number Of Parcels on Way To The Customer:{CounterParcelsOnMyWay}\n";
            }
        } 
    }
}
