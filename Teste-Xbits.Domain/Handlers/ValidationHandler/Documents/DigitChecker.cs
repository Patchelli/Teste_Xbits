namespace Teste_Xbits.Domain.Handlers.ValidationHandler.Documents;
public class DigitChecker(
    string number)
{
    private const int Module = 11;
    private const bool ModelAddOn = true;
    private readonly List<int> _multipliers = [2, 3, 4, 5, 6, 7, 8, 9];
    private readonly IDictionary<int, string> _replacement = new Dictionary<int, string>();

    public DigitChecker WithMultipliersUpTo(int firstMultiplier, int lastMultiplier)
    {
        _multipliers.Clear();

        for (var i = firstMultiplier; i <= lastMultiplier; i++)
            _multipliers.Add(i);

        return this;
    }

    public DigitChecker Replacing(string substitute, params int[] digits)
    {
        foreach (var i in digits)
            _replacement[i] = substitute;


        return this;
    }

    public void AddDigit(string digit)
    {
        number = string.Concat(number, digit);
    }

    public string CalculateDigit()
    {
        return !(number.Length > 0) ? "" : GetDigitSum();
    }

    private string GetDigitSum()
    {
        var sum = 0;

        for (int i = number.Length - 1, m = 0; i >= 0; i--)
        {
            var product = (int)char.GetNumericValue(number[i]) * _multipliers[m];
            sum += product;

            if (++m >= _multipliers.Count) m = 0;
        }

        var mod = (sum % Module);
        var result = ModelAddOn ? Module - mod : mod;

        return _replacement.TryGetValue(result, out var value) ? value : result.ToString();
    }
}
