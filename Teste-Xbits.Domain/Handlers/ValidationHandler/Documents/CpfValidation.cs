using Teste_Xbits.Domain.Extensions;

namespace Teste_Xbits.Domain.Handlers.ValidationHandler.Documents;

public class CpfValidation
{
    private const int CpfSize = 11;

    public static bool Validate(string cpf)
    {
        var cpfNumbers = cpf.OnlyNumbers();

        if (!HasLengthValid(cpfNumbers)) return false;

        return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
    }

    private static bool HasLengthValid(string value)
    {
        return value.Length == CpfSize;
    }

    private static bool HasRepeatedDigits(string value)
    {
        string[] invalidNumbers =
        {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
        return invalidNumbers.Contains(value);
    }

    private static bool HasValidDigits(string valor)
    {
        var number = valor.Substring(0, CpfSize - 2);

        var digitChecker = new DigitChecker(number).WithMultipliersUpTo(2, 11)
                                                   .Replacing("0", 10, 11);
           
        var firstDigit = digitChecker.CalculateDigit();

        digitChecker.AddDigit(firstDigit);

        var secondDigit = digitChecker.CalculateDigit();

        return string.Concat(firstDigit, secondDigit) == valor.Substring(CpfSize - 2, 2);
    }
}
