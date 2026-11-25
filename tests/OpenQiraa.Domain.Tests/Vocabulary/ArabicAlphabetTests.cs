using OpenQiraa.Domain.Vocabulary;
namespace OpenQiraa.Domain.Tests.Vocabulary;
using OpenQiraa.Domain.Vocabulary;

public class ArabicAlphabet
{
    [Theory]
    [InlineData('ً')] // tanwin fatha
    [InlineData('َ')] // fatha
    [InlineData('ّ')] // chadda
    [InlineData('ْ')] // soukoun
    public void IsHaraka_WithHaraka_ReturnsTrue(char c)
    {
        Func<char, bool> isHaraka = Domain.Vocabulary.ArabicAlphabet.isHaraka()
    }

    [Theory]
    [InlineData('ي')] // ي ,ya
    [InlineData('ٓ')] // soukoun
    [InlineData('a')] 
    public void IsHaraka_WithNonHaraka_ReturnsFalse(char c)    
}