using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Sente.Utils
{
    public class Deserializer
    {
        public T Deserialize<T>(string path)
        {
            XmlSerializer participantSerializer = new XmlSerializer(typeof(T));

            T deserialized;

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                deserialized = (T)participantSerializer.Deserialize(stream);
            }

            return deserialized;
        }
    }
}
