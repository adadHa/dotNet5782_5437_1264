using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal(string dataFormat)
        {
            try
            {
                IDal dal = null;
                Assembly testAss;
                string path;
                switch (dataFormat)
                {
                    case "DalObject":
                        // get the DalObject path
                        path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, 
                            "..\\..\\..\\..\\DalObject\\bin\\Debug\\net5.0\\DalObject.dll"));
                        break;
                    case "DalXml":
                        // get the DalXml path
                        path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                            "..\\..\\..\\..\\DalXml\\bin\\Debug\\net5.0\\DalXml.dll"));
                        break;
                    default:
                        return null;
                }
                testAss = Assembly.LoadFrom(path);
                foreach (Type type in testAss.GetTypes())
                {
                    if (type.IsClass == true)
                    {
                        Console.WriteLine($"Found Class: {type.FullName}");
                    }
                    //if (type.GetInterface("DalApi.IDal") == null)
                    //{
                    //    continue;
                    //}
                    if (type.Name == "DalObject"|| type.Name == "DalXml")
                    {
                        dal = (IDal)Activator.CreateInstance(type);
                        break;
                    }
                }
                return dal;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
//string basePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
