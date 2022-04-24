namespace MagicBinder.Core.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToLowerInvariant(text[0]) + text.Substring(1);
    }
}