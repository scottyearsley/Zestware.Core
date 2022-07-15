using System;
using System.Threading.Tasks;
using Xunit;
using Zestware.Data;

namespace Zestware.Core.UnitTests.Data;

public class HasherTests
{
    [Fact]
    public void xxHash_validValue_hexStringReturned()
    {
        var result = Hasher.XxHash("some text value");
        Assert.Equal("0e011c3a", result);
    }
    
    [Fact]
    public async Task xxHashAsync_validValue_hexStringReturned()
    {
        var result = await Hasher.XxHashAsync("some text value");
        Assert.Equal("0e011c3a", result);
    }

    [Fact]
    public void xxHash_nullPassed_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Hasher.XxHash(null));
    }
    
    [Fact]
    public async Task xxHashAsync_nullPassed_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Hasher.XxHashAsync(null));
    }
}