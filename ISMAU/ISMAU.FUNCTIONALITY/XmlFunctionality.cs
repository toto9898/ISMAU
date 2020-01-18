using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ISMAU.FUNCTIONALITY
{
    public static class XmlFunctionality
    {
        public static bool Serialize<T>(this T obj, string filePath)
        {
            if (obj == null) return false;

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(filePath))
            {
                xs.Serialize(wr, obj);
            }

            return true;
        }
        public static T Deserialize<T>(this T obj, string filePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)xs.Deserialize(reader);
            }
        }
    }
}
