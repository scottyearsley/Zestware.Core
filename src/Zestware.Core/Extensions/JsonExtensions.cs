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
        private static readonly Lazy<JsonSerializerOptions> DefaultSerializationOptions =
            new(() => new JsonSerializerOptions(JsonSerializerDefaults.Web));

        /// <summary>
        /// Serialize an object to JSON with camel-casing.
        /// </summary>
        /// <param name="this">The object instance to serialize.</param>
        /// <param name="prettyPrint">Render the JSON with indents.</param>
        /// <returns>The serialized object as a JSON string.</returns>
        public static string? ToJson(this object? @this, bool prettyPrint = false)
        {
            return @this != null 
                ? JsonSerializer.Serialize(@this, GetSerializationOptions(prettyPrint)) 
                : null;
        }
        
        /// <summary>
        /// Deserializes a JSON string to the specified object type instance.
        /// </summary>
        /// <typeparam name="T">The object class type.</typeparam>
        /// <param name="this">The JSON string to deserialize.</param>
        /// <returns>The deserialized object instance.</returns>
        public static T? FromJson<T>(this string? @this) where T: class
        {
            return @this != null
                ? JsonSerializer.Deserialize<T>(@this, GetSerializationOptions())
                : null;
        }

        /// <summary>
        /// Deserializes a JSON string to specified object type instance.
        /// </summary>
        /// <param name="this">The JSON string to deserialize.</param>
        /// <returns>The deserialized object instance.</returns>
        public static dynamic? FromJson(this string? @this)
        {
            return @this != null
                ? JsonSerializer.Deserialize<dynamic>(@this, GetSerializationOptions(allowDynamic: true))
                : null;
        }

        private static JsonSerializerOptions GetSerializationOptions(
            bool prettyPrint = false,
            bool allowDynamic = false)
        {
            if (!prettyPrint && !allowDynamic)
            {
                return DefaultSerializationOptions.Value;
            }
            
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            if (prettyPrint)
            {
                options.WriteIndented = true;
            }
            if (allowDynamic)
            {
                options.Converters.Add(new DynamicJsonConverter());
            }

            return options;
        }
    }
}