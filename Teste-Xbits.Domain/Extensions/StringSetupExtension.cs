using System.Text.RegularExpressions;

namespace Teste_Xbits.Domain.Extensions;

public static partial class StringSetupExtension
{
    public static string OnlyNumbers(this string value) =>
        !string.IsNullOrWhiteSpace(value) ? Numeric().Replace(value, "") : string.Empty;

    public static string OnlyAlphaNumeric(this string value) =>
        !string.IsNullOrWhiteSpace(value) ? AlphaNumeric().Replace(value, "") : string.Empty;
    
    public static string OnlyOnuCode(this string value) 
    {
        if (string.IsNullOrWhiteSpace(value)) 
            return string.Empty;
        
        var match = OnuCode().Match(value);
        
        return match.Success 
            ? match.Value 
            : string.Empty;
    }

    public static string DefineValueOrMaintainDataIntegrity(this string? newData, string currentData) =>
        string.IsNullOrWhiteSpace(newData) ? currentData : newData;

    public static string XmlSafeString(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }

    public static string SafeSubstring(this string value, int start, int length) =>
        start + length <= value.Length
            ? value.Substring(start, length)
            : string.Empty;

    public static string ToSafeSubstring(this int? value) =>
        value.ToString() ?? string.Empty;

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex Numeric();
    
    [GeneratedRegex("[^0-9a-zA-Z]+")]
    private static partial Regex AlphaNumeric();

    [GeneratedRegex(@"\d{4}")]
    private static partial Regex OnuCode();
}