using Xunit;
using Zestware.Collections;

namespace Zestware.Core.UnitTests.Collections;

public class CaseInsensitiveDictionaryTests
{
    [Fact]
    public void Get_KeyExistsWithDifferentCasing_ReturnsValue()
    {
        var dictionary = new CaseInsensitiveDictionary<string>
        {
            { "TheKey", "TheValue" }
        };
        
        Assert.NotNull(dictionary["theKey"]);
    }
}