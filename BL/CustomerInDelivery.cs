﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class CustomerInDelivery
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}";
            }
        } 
    }
}