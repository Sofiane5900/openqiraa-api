using OpenQiraa.Domain.Vocabulary;

namespace OpenQiraa.Domain.Tests.Vocabulary;

public class ArabicAlphabetTests
{
    [Theory]
    [InlineData('ء')] // hamza, first letter
    [InlineData('ك')] // kaf, middle of range
    [InlineData('ي')] // ya, last letter
    public void isLetter_WithLetter_ReturnsTrue(char c)
    {
        Assert.True(ArabicAlphabet.IsLetter(c));
    }

    [Theory]
    [InlineData('ؠ')] // just before hamza
    [InlineData('ً')] // just after ya (fathatan, first haraka)
    [InlineData('َ')] // fatha
    [InlineData('ـ')] // tatweel, in range but excluded
    [InlineData('a')] // latin
    public void isLetter_WithNonLetter_ReturnsFalse(char c)
    {
        Assert.False(ArabicAlphabet.IsLetter(c));
    }

    [Theory]
    [InlineData('ً')] // tanwin fatha
    [InlineData('َ')] // fatha
    [InlineData('ّ')] // chadda
    [InlineData('ْ')] // soukoun
    public void IsHaraka_WithHaraka_ReturnsTrue(char c)
    {
        Assert.True(ArabicAlphabet.isHaraka(c));
    }

    [Theory]
    [InlineData('ي')] // ي ,ya
    [InlineData('ٓ')] // soukoun
    [InlineData('a')]
    public void IsHaraka_WithNonHaraka_ReturnsFalse(char c)
    {
        Assert.False(ArabicAlphabet.isHaraka(c));
    }

    [Theory]
    [InlineData('ك')] // ك, letter
    [InlineData('ء')] // ء, first letter
    [InlineData('َ')] // fatha, haraka
    [InlineData('ْ')] // soukoun, last haraka
    public void IsLetterOrHaraka_WithLetterOrHaraka_ReturnsTrue(char c)
    {
        Assert.True(ArabicAlphabet.IsLetterOrHaraka(c));
    }

    [Theory]
    [InlineData('a')] // latin
    [InlineData('1')] // number
    [InlineData('ـ')] // tatweel
    [InlineData(' ')] // space
    public void IsLetterOrHaraka_WithOtherCharacter_ReturnsFalse(char c)
    {
        Assert.False(ArabicAlphabet.IsLetterOrHaraka(c));
    }
}
