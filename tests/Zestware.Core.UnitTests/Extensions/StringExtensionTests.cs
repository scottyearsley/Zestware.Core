using System;
using Xunit;

namespace Zestware.Core.UnitTests.Extensions;

public class StringExtensionTests
{
    [Theory]
    [InlineData("Value","value")]
    [InlineData("vAlUe", "value")]
    [InlineData("VALUE1", "value1")]
    public void EqualsCaseInsensitive_equal(string input, string comparison)
    {
        Assert.True(input.EqualsCaseInsensitive(comparison));
    }
    
    [Theory]
    [InlineData("Value","valuex")]
    [InlineData("vAlUe", "valuex")]
    [InlineData("VALUE1", "value1x")]
    public void EqualsCaseInsensitive_not_equal(string input, string comparison)
    {
        Assert.False(input.EqualsCaseInsensitive(comparison));
    }

    [Theory]
    [InlineData("A simple sentence", "aSimpleSentence")]
    [InlineData("ABC", "abc")]
    [InlineData("dashed-value", "dashedValue")]
    [InlineData("1Value", "1value")]
    [InlineData("more   whitespace", "moreWhitespace")]
    [InlineData("1", "1")]
    [InlineData("a", "a")]
    [InlineData(null, null, true)]
    public void ToCamelCase(string input, string expected, bool argumentNullExceptionExpected = false)
    {
        if (argumentNullExceptionExpected)
        {
            Assert.Throws<ArgumentNullException>(input.ToCamelCase);
        }
        else
        {
            var result = input.ToCamelCase();
            Assert.Equal(expected, result);
        }
    }
    
    [Theory]
    [InlineData("A simple sentence", "ASimpleSentence")]
    [InlineData("ABC", "ABC")]
    [InlineData("dashed-value", "DashedValue")]
    [InlineData("1Value", "1Value")]
    [InlineData("more   whitespace", "MoreWhitespace")]
    [InlineData("1", "1")]
    [InlineData("a", "A")]
    [InlineData(null, null, true)]
    public void ToPascalCase(string input, string expected, bool argumentNullExceptionExpected = false)
    {
        if (argumentNullExceptionExpected)
        {
            Assert.Throws<ArgumentNullException>(input.ToPascalCase);
        }
        else
        {
            var result = input.ToPascalCase();
            Assert.Equal(expected, result);
        }
    }
}
