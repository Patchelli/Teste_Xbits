namespace Teste_Xbits.Domain.Extensions;

public static class InterpolationExtension
{
    public static string FormatTo(this string message, params object[] args) => string.Format(message, args);
}