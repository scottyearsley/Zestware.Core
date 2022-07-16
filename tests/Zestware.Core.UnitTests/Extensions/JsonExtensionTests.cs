﻿using Xunit;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Zestware.Core.UnitTests.Extensions;

public class JsonExtensionTests
{
    private class KeyValue
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

    [Fact]
    public void ToJson_object_returnsJson()
    {
        var obj = new KeyValue { Key = "colour", Value = "red" };
        var result = obj.ToJson();
        Assert.NotNull(result);
        Assert.Equal(@"{""key"":""colour"",""value"":""red""}", result);
    }
    
    [Fact]
    public void ToJson_null_returnsJson()
    {
        KeyValue obj = null!;
        var result = obj.ToJson();
        Assert.Null(result);
    }
    
    [Fact]
    public void FromJson_typed_returnsObject()
    {
        var jsonString = @"{""key"": ""colour"", ""value"": ""red""}";
        var result = jsonString.FromJson<KeyValue>();
        
        Assert.NotNull(result);
        Assert.Equal("colour", result!.Key);
        Assert.Equal("red", result.Value);
    }
    
    [Fact]
    public void FromJson_untyped_returnsDynamic()
    {
        var jsonString = @"{""key"": ""colour"", ""value"": ""red""}";
        var result = jsonString.FromJson();
        
        Assert.NotNull(result);
        Assert.Equal("colour", (string)result!.key);
        Assert.Equal("red", (string)result.value);
    }
}