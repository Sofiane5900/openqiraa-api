using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Entities;

namespace OpenQiraa.Domain.Tests.Vocabulary;

public class WordTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t\n")] // whitespace only
    public void Constructor_WithNullOrWhiteSpaceArabicText_ThrowsDomainException(string? arabicText)
    {
        Action act = () => new Word(arabicText!, Guid.NewGuid());
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData(" كتاب ", "كتاب")] // kitab, spaces both sides
    [InlineData("يكتب   ", "يكتب")] // yaktub, trailing spaces
    [InlineData("\tيَكْتُبُ\n", "يَكْتُبُ")] // yaktubu with harakat, tab + newline
    public void Constructor_WithSurroundingSpace_StoresTrimmedArabicText(
        string input,
        string excepted
    )
    {
        Word word = new Word(input, Guid.NewGuid());

        Assert.Equal(excepted, word.ArabicText);
    }

    [Theory]
    [InlineData("book")] // latin
    [InlineData("كتاب2")] // kitab + digit
    [InlineData("كـتاب")] // tatweel inside
    [InlineData("كتاب?")] // punctuation
    [InlineData("كت اب")] // space in the middle
    public void Constructor_WithNonArabicLettersOrHarakats_ThrowsDomainExcepton(string? arabicText)
    {
        Action act = () => new Word(arabicText!, Guid.NewGuid());
        Assert.Throws<DomainException>(act);
    }
}
