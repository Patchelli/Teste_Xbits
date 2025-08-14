namespace Teste_Xbits.Domain.Extensions;

public static class NumericValidationExtension
{
    public static bool NumericalProportionInFloatValidation(this float value, float min = 0, float max = 10)
    {
        if (value < min) return false;
            
        return !(value > max);
    }
    
    public static bool NumericalProportionInIntegerValidation(this int value, int min = 0, int max = 10)
    {
        if (value < min) return false;
            
        return !(value > max);
    }
    
    public static int? CheckCodeMeter(this int? thisMeter, int? comparisonMeter)
    {
        if (thisMeter is not null && comparisonMeter is not null) return thisMeter;
            
        return null;
    }
}