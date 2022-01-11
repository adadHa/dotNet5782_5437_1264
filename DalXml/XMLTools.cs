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

        #region Tolls for xml element
        public static void SaveListToXMLElement(XElement xElement, string path)
        {
            try
            {
                xElement.Save(pathToDir +path);
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(path, $"fail to create xml file: {path}", ex);
            }
        }
        public static XElement LoadListFromXMLElement(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return XElement.Load(pathToDir + path);
                }
                else
                {
                    XElement rootElement = new XElement(pathToDir + path);
                    rootElement.Save(pathToDir + path);
                    return rootElement;
                }
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException(path, $"fail to create xml file: {path}", ex);
            }
        }
        #endregion

        #region Tools for xml serilaizer
        public static void SaveListToXMLSerializer<T>(List<T> listToSave, string pathToSave)
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

        public static List<T> LoadListFromXMLSerializer<T>(string fileToLoad)
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
        #endregion
    }
}
