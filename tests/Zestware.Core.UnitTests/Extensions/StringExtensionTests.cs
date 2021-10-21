using Xunit;

namespace Zestware.Core.UnitTests.Extensions
{
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
        public void ToCamelCase(string input, string expected)
        {
            Assert.Equal(expected, input.ToCamelCase());
        }
    }
}