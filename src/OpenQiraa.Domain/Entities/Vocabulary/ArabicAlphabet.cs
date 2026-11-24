namespace OpenQiraa.Domain.Vocabulary;

public static class ArabicAlphabet
{
    private const char FirstLetter = 'ء'; // ء hamza
    private const char LastLetter = 'ي'; // ي ya
    private const char Tatweel = 'ـ'; // ـ tr

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
}
