using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Entities;

namespace OpenQiraa.Domain.Tests.Vocabulary;

public class LemmaTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_WithNullOrEmptyArabicText_ThrowsDomainException(string? arabicText)
    {
        Action act = () => new Lemma(arabicText!, "to sit", Guid.NewGuid());
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData(" كتب ", "كتب")] // spaces both sides
    [InlineData("كتب   ", "كتب")] // trailing spaces
    [InlineData("\tكَتَبَ\n", "كَتَبَ")] // tab + newline, with harakat
    public void Constructor_WithSurroundingSpace_StoresTrimmedArabicText(
        string? input,
        string? expected
    )
    {
        Lemma newLemma = new Lemma(input!, "to eat", Guid.NewGuid());
        Assert.Equal(expected, newLemma.ArabicText);
    }

    [Theory]
    [InlineData("hello")] // latin
    [InlineData("كَتَبَ1")] // kataba + digit
    [InlineData("كـتب")] // tatweel inside
    [InlineData("كتب!")] // punctuation
    public void Constructor_WithNonArabicLettersOrHarakats_ThrowsDomainException(string? arabicText)
    {
        Action act = () => new Lemma(arabicText!, "to sit", Guid.NewGuid());
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_WithoutDefinition_ThrowsDomainException(string? definition)
    {
        Action act = () => new Lemma("كتب", definition!, Guid.NewGuid());
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData(" to write ", "to write")] // spaces both sides
    [InlineData("knowledge   ", "knowledge")] // trailing spaces
    [InlineData("\tto write\n", "to write")] // tab + newline
    public void Constructor_WithSurroundingSpace_StoresTrimmedDefinition(
        string? input,
        string? expected
    )
    {
        Lemma newLemma = new Lemma("عِلْم", input!, Guid.NewGuid());
        Assert.Equal(expected, newLemma.Definition);
    }

    [Fact]
    public void Constructor_WithoutRootId_ThrowsDomainException()
    {
        Action act = () => new Lemma("كتب", "to write", Guid.Empty);
        Assert.Throws<DomainException>(act);
    }

    [Theory]
    [InlineData("كَتَبَ", "to write")] // kataba, fatha
    [InlineData("عِلْم", "knowledge")] // ilm, kasra + sukun
    [InlineData("مُحَمَّد", "praised")] // muhammad, damma + shadda
    [InlineData("كتب", "to write")] // without harakat
    public void Constructor_WithValidParameters_CreatesLemma(string arabicText, string definition)
    {
        Guid rootId = Guid.NewGuid();
        Lemma newLemma = new Lemma(arabicText, definition, rootId);
        Assert.Equal(arabicText, newLemma.ArabicText);
        Assert.Equal(definition, newLemma.Definition);
        Assert.Equal(rootId, newLemma.RootId);
    }
}
