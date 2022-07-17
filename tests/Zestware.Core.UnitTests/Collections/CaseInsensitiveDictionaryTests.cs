using Xunit;
using Zestware.Collections;

namespace Zestware.Core.UnitTests.Collections;

public class CaseInsensitiveDictionaryTests
{
    [Fact]
    public void GetValueCaseInsensitive()
    {
        var dictionary = new CaseInsensitiveDictionary<string>
        {
            { "TheKey", "TheValue" }
        };
        
        Assert.NotNull(dictionary["theKey"]);
    }
}