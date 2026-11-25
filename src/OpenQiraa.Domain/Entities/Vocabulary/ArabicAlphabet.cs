namespace OpenQiraa.Domain.Vocabulary;

public static class ArabicAlphabet
{
    private const char FirstLetter = 'ء'; // ء hamza
    private const char LastLetter = 'ي'; // ي ya
    private const char Tatweel = 'ـ'; // ـ tr

    private const char FirstHaraka = 'ً'; // tanwin fath (ً)
    private const char LastHaraka = 'ْ'; // soukoun (ْ)

    public static bool IsLetter(char c)
    {
        if (c == Tatweel)
        {
            return false;
        }

        if (c < FirstLetter || c > LastLetter)
        {
            return false;
        }

        return true;
    }

    public static bool isHaraka(char c)
    {
        if (c < FirstHaraka || c > LastHaraka)
        {
            return false;
        }

        return true;
    }

    public static bool IsLetterOrHaraka(char c)
    {
        if (IsLetter(c) || isHaraka(c))
        {
            return true;
        }

        return false;
    }
}
