using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Entities;

public sealed class Word : BaseEntity
{
    public string ArabicText { get; private set; }
    public Guid LemmaId { get; set; }

    public Word(string arabicText, Guid lemmaId)
    {
        if (string.IsNullOrWhiteSpace(arabicText))
            throw new DomainException("Le texte est requis.");
        ArabicText = arabicText.Trim();
        if (lemmaId == Guid.Empty)
            throw new DomainException("Le lemme est requis.");
        LemmaId = lemmaId;
    }
}
