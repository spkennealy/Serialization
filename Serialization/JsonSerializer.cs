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
    }
}
