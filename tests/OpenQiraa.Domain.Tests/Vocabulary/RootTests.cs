using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Tests.Vocabulary;

using OpenQiraa.Domain.Entities;

public class RootTests
{
    [Theory]
    [InlineData("كت")]
    [InlineData("غضنفرغ")]
    public void Constructor_WithInvalidLength_ThrowsDomainException(string letters)
    {
        Action act = () => new Root(letters);
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData("كتب")]
    [InlineData("زلزل")]
    [InlineData("سفرجل")]
    public void Constructor_WithValidLetters_CreatesRoot(string letters)
    {
        Root root = new(letters);
        Assert.Equal(letters, root.ArabicLetters);
    }

    [Fact]
    public void Constructor_WithEmptyString_ThrowsDomainException()
    {
        string letters = " ";

        Action act = () => new Root(letters);

        Assert.Throws<DomainException>(act);
    }
}
