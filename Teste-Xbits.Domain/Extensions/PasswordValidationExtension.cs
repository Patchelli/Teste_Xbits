using System.Text.RegularExpressions;

namespace Teste_Xbits.Domain.Extensions;

public static partial class PasswordValidationExtension
{
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$")]
    private static partial Regex PasswordWithRegex();
    
    public static bool ValidatePassword(this string? password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var regex = PasswordWithRegex();
        var match = regex.Match(password);
        return match.Success;
    }
    
}
