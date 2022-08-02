using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Zestware.Async;

namespace Zestware.Core.UnitTests.Async;

public class AsyncHelperTests
{
    private readonly ITestOutputHelper _output;

    public AsyncHelperTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void RunSync_Task()
    {
        var now = DateTime.Now;
        AsyncHelper.RunSync(AsyncMethod);
        var diff = DateTime.Now - now;
        _output.WriteLine("Completed in {0}ms", diff.Milliseconds);
        Assert.True(diff.Milliseconds >= 100);
    }
    
    [Fact]
    public void RunSync_TaskOfT()
    {
        var now = DateTime.Now;
        var result = AsyncHelper.RunSync(() => AsyncMethod(3));
        var diff = DateTime.Now - now;
        _output.WriteLine("Completed in {0}ms", diff.Milliseconds);
        Assert.True(diff.Milliseconds >= 100);
        Assert.Equal(3, result);
    }

    private async Task AsyncMethod()
    {
        await Task.Delay(110);
    }
    
    private async Task<int> AsyncMethod(int value)
    {
        await Task.Delay(100);
        return value;
    }
}