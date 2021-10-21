using System;
using System.Text.Json;

namespace Zestware
{
    public static class JsonExtensions
    {
        private static readonly Lazy<JsonSerializerOptions> SerializationOptions =
            new(() => new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
        public static string ToJson(this object @this)
        {
            return @this != null 
                ? JsonSerializer.Serialize(@this, SerializationOptions.Value) 
                : null;
        }

        public static T FromJson<T>(this string @this) where T: class
        {
            return @this != null
                ? JsonSerializer.Deserialize<T>(@this, SerializationOptions.Value)
                : null;
        }
    }
}