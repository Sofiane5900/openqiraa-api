using OpenQiraa.Domain.Common;

namespace OpenQiraa.Domain.Entities;

public class Lemma : BaseEntity
{
    public string ArabicText { get; private set; }
    public string Definition { get; private set; }
    public Guid RootId { get; private set; }

    public Lemma(string arabicText, string definition, Guid rootId)
    {
        if (string.IsNullOrWhiteSpace(arabicText))
            throw new DomainException("Le texte est requis.");
        ArabicText = arabicText.Trim();
        if (string.IsNullOrWhiteSpace(definition))
            throw new DomainException("La définition est requise.");
        Definition = definition.Trim();
        if (rootId == Guid.Empty)
            throw new DomainException("Les racines sont requises.");
        RootId = rootId;
    }
}
