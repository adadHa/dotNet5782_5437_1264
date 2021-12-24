using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;

namespace Dal
{
    public sealed partial class DalObject : DalApi.IDal
    {
        //implement Lazy Instantiation (and thread-safe) singleton design pattern
        // Lazy - create the BL entity only when called
        // thread safe - the Nested class loader will do all static initialization before
        //               it will enable other threads accessing the class
        public DalObject()
        {
            DataSource.Initialize();
        }

        public static DalObject Instance { get { return Nested.Instance; } }
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly DalObject Instance = new DalObject();
        }
    }
}