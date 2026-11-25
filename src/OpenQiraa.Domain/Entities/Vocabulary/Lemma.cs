using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Vocabulary;

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
        var arabicTextTrimmed = arabicText.Trim();

        if (!arabicTextTrimmed.All(ArabicAlphabet.IsLetterOrHaraka))
        {
            throw new DomainException(
                "A Lemma" + " should consist of Arabic letters or harakats only."
            );
        }
        if (string.IsNullOrWhiteSpace(definition))
        {
            throw new DomainException("A definition is required.");
        }
        var definitionTrimmed = definition.Trim();

        if (rootId == Guid.Empty)
        {
            throw new DomainException("A root is required.");
        }

        ArabicText = arabicTextTrimmed;

        Definition = definitionTrimmed;

        RootId = rootId;
    }
}
