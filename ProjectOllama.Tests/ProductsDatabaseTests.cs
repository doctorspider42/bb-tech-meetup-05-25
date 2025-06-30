using ProjectOllama;
using Xunit;

namespace ProjectOllama.Tests;

public class ProductsDatabaseTests
{
    [Theory]
    [InlineData("SUPER2000")]
    [InlineData("super2000")]
    [InlineData(" Super 2000 ")]
    public void Find_ReturnsInfo_ForExactAndCaseInsensitiveMatches(string query)
    {
        var result = ProductsDatabase.Find(query);
        Assert.NotNull(result);
        Assert.Contains("SUPER2000", result!, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("DATAMAX PRO")]
    [InlineData("Data Max Pro")]
    [InlineData("data max")]
    public void Find_ReturnsInfo_ForPartialMatches(string query)
    {
        var result = ProductsDatabase.Find(query);
        Assert.NotNull(result);
        Assert.Contains("DATAMAX PRO", result!, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Find_ReturnsNull_ForUnknownProduct()
    {
        var result = ProductsDatabase.Find("unknown product");
        Assert.Null(result);
    }
}
