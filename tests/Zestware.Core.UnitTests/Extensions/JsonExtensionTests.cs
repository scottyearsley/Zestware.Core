using System;
using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Zestware.Core.UnitTests.Extensions;

public class JsonExtensionTests
{
    private static readonly string NewLine = Environment.NewLine;
    
    [Fact]
    public void ToJson_ValidObject_ReturnsFlatJson()
    {
        var obj = new KeyValue { Key = "colour", Value = "red" };
        var result = obj.ToJson();
        Assert.NotNull(result);
        Assert.Equal("{\"key\":\"colour\",\"value\":\"red\"}", result);
    }
    
    [Fact]
    public void ToJson_PrettyPrint_ReturnsIndentedJson()
    {
        var obj = new KeyValue { Key = "colour", Value = "red" };
        var result = obj.ToJson(true);
        Assert.NotNull(result);
        Assert.Equal($"{{{NewLine}  \"key\": \"colour\",{NewLine}  \"value\": \"red\"{NewLine}}}", result);
    }

    [Fact]
    public void ToJson_NullObject_ReturnsNull()
    {
        KeyValue obj = null!;
        var result = obj.ToJson();
        Assert.Null(result);
    }

    [Fact]
    public void FromJson_Typed_ReturnsObject()
    {
        var jsonString = @"{""key"": ""colour"", ""value"": ""red""}";
        var result = jsonString.FromJson<KeyValue>();
        
        Assert.NotNull(result);
        Assert.Equal("colour", result!.Key);
        Assert.Equal("red", result.Value);
    }

    [Fact]
    public void FromJson_Untyped_ReturnsDynamic()
    {
        var jsonString = @"{""key"": ""colour"", ""value"": ""red""}";
        var result = jsonString.FromJson();
        
        Assert.NotNull(result);
        Assert.Equal("colour", (string)result!.key);
        Assert.Equal("red", (string)result.value);
    }

    private class KeyValue
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}