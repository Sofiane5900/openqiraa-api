using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Entities;

public class Root : BaseEntity
{
    public string ArabicLetters { get; private set; }

    public Root(string arabicLetters)
    {
        var letters = arabicLetters.Trim();

        if (letters.Length < 3 || letters.Length > 5)
            throw new DomainException(
                "Roots should not be less than 3 letters, or more than 5 letters."
            );
        if (string.IsNullOrWhiteSpace(letters))
            throw new DomainException("Letters are required.");

        ArabicLetters = letters;
    }
}
