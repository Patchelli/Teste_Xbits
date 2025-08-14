namespace Teste_Xbits.Domain.Extensions;

public static class ParseExtension
{
    public static int? SecureParseInt(this string? value)
    {
        var onlyNumbers = value?.OnlyNumbers();

        if (!string.IsNullOrEmpty(onlyNumbers) && int.TryParse(onlyNumbers, out var parsedLength))
            return parsedLength;

        return null;
    }

    public static long SecureParseLong(this string? value)
    {
        var onlyNumbers = value?.OnlyNumbers();

        if (!string.IsNullOrEmpty(onlyNumbers) && long.TryParse(onlyNumbers, out var parsedLength))
            return parsedLength;

        return 0;
    }
}