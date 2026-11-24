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
        {
            throw new DomainException("A text is required.");
        }
        ArabicText = arabicText.Trim();
        if (string.IsNullOrWhiteSpace(definition))
            throw new DomainException("A definition is required.");
        Definition = definition.Trim();
        if (rootId == Guid.Empty)
            throw new DomainException("Roots are required.");
        RootId = rootId;
    }
}
