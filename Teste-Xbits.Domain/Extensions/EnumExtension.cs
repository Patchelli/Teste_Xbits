using System.ComponentModel;
using System.Reflection;

namespace Teste_Xbits.Domain.Extensions;

public static class EnumExtension
{
    public static string GetDescription<T>(this T message) where T : Enum
    {
        var type = message.GetType();
        var memberInfo = type.GetMember(message.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return ((DescriptionAttribute)attributes[0]).Description;
    }

    public static T? GetEnum<T>(this string description) where T : Enum
    {
        var type = typeof(T);

        if (!type.GetTypeInfo().IsEnum)
            throw new ArgumentException();

        var field = type
            .GetFields()
            .SelectMany(f => f.GetCustomAttributes(
                typeof(DescriptionAttribute), false), 
                (f, a) => new { Field = f, Att = a })
            .SingleOrDefault(a => ((DescriptionAttribute)a.Att).Description == description);

        return field == null 
            ? default 
            : (T)field.Field.GetRawConstantValue()!;
    }
    
    public static T? GetEnumByString<T>(this string value) where T : Enum
    {
        var type = typeof(T);

        if (!type.GetTypeInfo().IsEnum)
            throw new ArgumentException();
                
        var values = (T[])Enum.GetValues(typeof(T));

        return values.FirstOrDefault(v => v.ToString() == value);
    }
}