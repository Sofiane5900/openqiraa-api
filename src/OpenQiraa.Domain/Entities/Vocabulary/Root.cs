using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Entities;

public class Root : BaseEntity
{
    public string ArabicLetters { get; private set; }

    public Root(string arabicLetters)
    {
        if (string.IsNullOrWhiteSpace(arabicLetters))
            throw new DomainException("Les lettres sont requises.");
        ArabicLetters = arabicLetters.Trim();
    }
}
