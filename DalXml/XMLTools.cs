using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
namespace DalXml
{
    static class XMLTools
    {
        static string pathToDir = @"xml/";
        static XMLTools()
        {
            Directory.CreateDirectory(pathToDir);
        }

        ///Tools for xml serilaizer
        static void SaveListToXMLSerializer<T>(List<T> listToSave, string pathToSave)
        {
            try
            {
                FileStream file = new FileStream(pathToDir + pathToSave, FileMode.Create);
                XmlSerializer xmlSerializer = new XmlSerializer(listToSave.GetType());
                xmlSerializer.Serialize(file, listToSave);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(pathToSave, $"fail to create xml file: {pathToSave}", ex);
            }
        }

        static List<T> LoadListFromXMLSerializer<T>(string fileToLoad)
        {
            try
            {
                if(File.Exists(pathToDir + fileToLoad))
                {
                    List<T> list;
                    FileStream file = new FileStream(pathToDir + fileToLoad, FileMode.Open);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                    list = (List<T>)xmlSerializer.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                {
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(fileToLoad, $"fail to create xml file: {fileToLoad}", ex);
            }
        }
    }
}
