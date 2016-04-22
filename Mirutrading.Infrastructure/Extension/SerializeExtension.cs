using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mirutrading.Infrastructure.Extension
{
    public static class SerializeExtension
    {
        public static string SerializeJson<T>(this T @this)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using(var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, @this);
                return Encoding.Default.GetString(memoryStream.ToArray());
            }
        }

        public static T DeserializeJson<T>(this string @this)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using(var stream = new MemoryStream(Encoding.Default.GetBytes(@this)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        public static string SerializeXml<T>(this T @this)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using(var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, @this);
                using(var streamReader = new StringReader(stringWriter.GetStringBuilder().ToString()))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static T DeserializeXml<T>(this string @this)
        {
            var x = new XmlSerializer(typeof(T));
            var r = new StringReader(@this);

            return (T)x.Deserialize(r);
        }
    }


}
