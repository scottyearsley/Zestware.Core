﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zestware.Json;

public class DynamicJsonConverter : JsonConverter<dynamic>
{
    public override dynamic? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.True)
        {
            return true;
        }

        if (reader.TokenType == JsonTokenType.False)
        {
            return false;
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetInt64(out var longValue))
            {
                return longValue;
            }

            return reader.GetDouble();
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            if (reader.TryGetDateTime(out var datetime))
            {
                return datetime;
            }

            return reader.GetString();
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            return ReadObject(jsonDocument.RootElement);
        }

        // Use JsonElement as fallback.
        // Newtonsoft uses JArray or JObject.
        var document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone();
    }

    private object ReadObject(JsonElement jsonElement)
    {
        IDictionary<string, object> expandoObject = new ExpandoObject()!;
        foreach (var obj in jsonElement.EnumerateObject())
        {
            var objName = obj.Name;
            var value = ReadValue(obj.Value);
            expandoObject[objName] = value!;
        }

        return expandoObject;
    }

    private object? ReadValue(JsonElement jsonElement)
    {
        object? result;
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Object:
                result = ReadObject(jsonElement);
                break;
            case JsonValueKind.Array:
                result = ReadList(jsonElement);
                break;
            case JsonValueKind.String:
                //TODO: Bytes Convert
                if (jsonElement.TryGetDateTime(out var dateTimeValue))
                {
                    result = dateTimeValue;
                    break;
                }
                result = jsonElement.GetString();
                break;
            case JsonValueKind.Number:
                //TODO: more num type
                result = 0;
                if (jsonElement.TryGetInt64(out var longValue))
                {
                    result = longValue;
                }
                if (jsonElement.TryGetDecimal(out var decimalValue))
                {
                    result = decimalValue;
                }
                if (jsonElement.TryGetDouble(out var doubleValue))
                {
                    result = doubleValue;
                }
                break;
            case JsonValueKind.True:
                result = true;
                break;
            case JsonValueKind.False:
                result = false;
                break;
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                result = null;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    private object? ReadList(JsonElement jsonElement)
    {
        IList<object?> list = new List<object?>();
        foreach (var item in jsonElement.EnumerateArray())
        {
            list.Add(ReadValue(item));
        }

        return list.Count == 0 ? null : list;
    }

    public override void Write(Utf8JsonWriter writer,
        object value,
        JsonSerializerOptions options)
    {
        // Unsupported
    }
}