using System;
using System.Threading.Tasks;
using Xunit;
using Zestware.Data;

namespace Zestware.Core.UnitTests.Data;

public class HasherTests
{
    [Fact]
    public void XxHash_ValidValue_ReturnsHexString()
    {
        var result = Hasher.XxHash("some text value");
        Assert.Equal("0e011c3a", result);
    }
    
    [Fact]
    public async Task XxHashAsync_ValidValue_ReturnsHexString()
    {
        var result = await Hasher.XxHashAsync("some text value");
        Assert.Equal("0e011c3a", result);
    }

    [Fact]
    public void XxHash_NullPassed_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Hasher.XxHash(null));
    }
    
    [Fact]
    public async Task XxHashAsync_NullPassed_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Hasher.XxHashAsync(null));
    }
}