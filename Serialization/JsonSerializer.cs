using System.Text;
using System.IO;
using Amazon.Lambda.Serialization.Json;
using System.Xml.Serialization;
using System;

namespace Serialization
{
    public static class Serializer
    {
        private static readonly JsonSerializer jsonSerializer = new JsonSerializer();

        public static string ToJson<T>(T input)
        {
            using (var ms = new MemoryStream())
            {
                jsonSerializer.Serialize(input, ms);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static T FromJson<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return jsonSerializer.Deserialize<T>(ms);
            }
        }

        public static T DeserializeXml<T>(object input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(input.ToString())))
            {
                return (T)ser.Deserialize(ms);
            }
        }

        public static string SerializeToXml<T>(T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(objectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, objectToSerialize);
                return textWriter.ToString();
            }
        }
    }

    public static class Utils
    {
        public static string EncodeBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string ExtractIdFromUrl(string url)
        {
            var splitUrl = url.Split('/');
            return splitUrl[splitUrl.Length - 1];
        }
    }
}
