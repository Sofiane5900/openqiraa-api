using OpenQiraa.Domain.Common;
using OpenQiraa.Domain.Vocabulary;

namespace OpenQiraa.Domain.Entities;

public sealed class Word : BaseEntity
{
    public string ArabicText { get; private set; }
    public Guid LemmaId { get; private set; }

    public Word(string arabicText, Guid lemmaId)
    {
        if (string.IsNullOrWhiteSpace(arabicText))
        {
            throw new DomainException("An arabic text is required.");
        }

        string arabicTextTrimmed = arabicText.Trim();

        if (!arabicTextTrimmed.All(ArabicAlphabet.IsLetterOrHaraka))
        {
            throw new DomainException(
                "A word" + " should only consist of Arabic letters or harakats."
            );
        }
        if (lemmaId == Guid.Empty)
        {
            throw new DomainException("A lemma is required.");
        }

        ArabicText = arabicTextTrimmed;
        LemmaId = lemmaId;
    }
}
