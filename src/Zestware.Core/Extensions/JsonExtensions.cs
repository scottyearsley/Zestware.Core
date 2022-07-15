using System;
using System.Text.Json;
using Zestware.Json;

namespace Zestware
{
    /// <summary>
    /// Simple helper methods for JSON conversions using System.Text.Json.
    /// </summary>
    public static class JsonExtensions
    {
        private static readonly Lazy<JsonSerializerOptions> SerializationOptions =
            new(() => new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters = { new DynamicJsonConverter() }
            });
        
        /// <summary>
        /// Serialize an object to JSON with camel-casing.
        /// </summary>
        /// <param name="this">The object instance to serialize.</param>
        /// <returns>The serialized object string as JSON.</returns>
        public static string? ToJson(this object? @this)
        {
            return @this != null 
                ? JsonSerializer.Serialize(@this, SerializationOptions.Value) 
                : null;
        }

        /// <summary>
        /// Deserialize a JSON string to .
        /// </summary>
        /// <param name="this">The object instance to serialize.</param>
        /// <returns>The serialized object string as JSON.</returns>
        public static T? FromJson<T>(this string? @this) where T: class
        {
            return @this != null
                ? JsonSerializer.Deserialize<T>(@this, SerializationOptions.Value)
                : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static dynamic? FromJson(this string? @this)
        {
            return FromJson<dynamic>(@this);
        }
    }
}