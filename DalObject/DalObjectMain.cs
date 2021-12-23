using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;

namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {
        //implement Lazy Instantiation (and thread-safe) singleton design pattern
        // Lazy - create the BL entity only when called
        // thread safe - the Nested class loader will do all static initialization before
        //               it will enable other threads access to the class
        public DalObject()
        {
            DataSource.Initialize();
        }
    }
}