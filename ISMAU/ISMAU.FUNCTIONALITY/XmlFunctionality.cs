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
    public static class XmlFunctionality
    {
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
