using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace ISMAU.FUNCTIONALITY
{
    /// <summary>
    /// Holds the logic for serializing and deserializing of objects
    /// </summary>
    public static class XmlFunctionality
    {
        /// <summary>
        /// Serializes the object a the file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">object for serialization</param>
        /// <param name="filePath">the path to the file for the serialization</param>
        /// <returns>True if the serialization is successful and False otherwise</returns>
        public static bool Serialize<T>(this T obj, string filePath)
        {
            if (obj == null) return false;

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamWriter wr = new StreamWriter(filePath))
                {
                    xs.Serialize(wr, obj);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to save the data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Serializes the object a the file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">object for serialization</param>
        /// <param name="filePath">the path to the file for the serialization</param>
        /// <returns>True if the serialization is successful and False otherwise</returns>
        public static T Deserialize<T>(this T obj, string filePath)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (T)xs.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load the data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

        }
    }
}
