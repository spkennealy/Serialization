using System.Text;
using System.IO;
using Amazon.Lambda.Serialization.Json;
using System.Xml.Serialization;
using System;

namespace Serialization
{
    public static class JsonSerializer
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
    }
}
