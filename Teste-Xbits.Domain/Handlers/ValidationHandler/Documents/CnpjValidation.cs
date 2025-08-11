using Teste_Xbits.Domain.Extensions;

namespace Teste_Xbits.Domain.Handlers.ValidationHandler.Documents;

public class CnpjValidation
{
    private const int CnpjSize = 14;

    public static bool Validate(string cnpj)
    {
        var cnpjNumbers = cnpj.OnlyNumbers();

        if (!HasLengthValid(cnpjNumbers)) return false;
            
        return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
    }

    private static bool HasLengthValid(string valor)
    {
        return valor.Length == CnpjSize;
    }

    private static bool HasRepeatedDigits(string valor)
    {
        string[] invalidNumbers =
        {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
        return invalidNumbers.Contains(valor);
    }

    private static bool HasValidDigits(string value)
    {
        var number = value[..(CnpjSize - 2)];

        var digitChecker = new DigitChecker(number).WithMultipliersUpTo(2, 9)
                                                   .Replacing("0", 10, 11);

        var firstDigit = digitChecker.CalculateDigit();

        digitChecker.AddDigit(firstDigit);

        var secondDigit = digitChecker.CalculateDigit();

        return string.Concat(firstDigit, secondDigit) == value.Substring(CnpjSize - 2, 2);
    }
}