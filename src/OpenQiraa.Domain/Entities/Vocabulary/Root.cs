using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Vocabulary;

namespace OpenQiraa.Domain.Entities;

public class Root : BaseEntity
{
    public string ArabicLetters { get; private set; }

    public Root(string? arabicLetters)
    {
        if (string.IsNullOrWhiteSpace(arabicLetters))
        {
            throw new DomainException("Letters are required.");
        }
        var letters = arabicLetters.Trim();

        if (!letters.All(ArabicAlphabet.IsLetter))
        {
            throw new DomainException("A root should consist of Arabic letters only.");
        }
        if (letters.Length < 3 || letters.Length > 5)
            throw new DomainException(
                "A root should not be less than 3 letters, or more than 5 letters."
            );

        ArabicLetters = letters;
    }
}
