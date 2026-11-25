using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Entities;

namespace OpenQiraa.Domain.Tests.Vocabulary;

public class RootTests
{
    [Theory]
    [InlineData("كتب ", "كتب")]
    [InlineData(" زلزل ", "زلزل")]
    [InlineData("كتب    ", "كتب")]
    public void Constructor_WithSurroundingSpace_StoresTrimmedLetters(
        string? input,
        string? expected
    )
    {
        Root root = new Root(input);
        Assert.Equal(expected, root.ArabicLetters);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("")]
    public void Constructor_WithNullOrEmptyString_ThrowsDomainException(string? letters)
    {
        Action act = () => new Root(letters);
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("كت1")]
    [InlineData("Arbre")]
    public void Construtor_WithNonArabicLetters_ThrowsDomainException(string letters)
    {
        Action act = () => new Root(letters);
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData("ت")]
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
}
