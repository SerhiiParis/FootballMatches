using System.ComponentModel;

namespace FootballMatches.Shared;

public static class EnumExtensions
{
    public static string GetDescriptionString<TEnum>(this TEnum @enum)
        where TEnum : Enum
    {
        var type = typeof(TEnum);

        var memberInfo = type.GetMember(type.GetEnumName(@enum)!);

        return memberInfo[0]
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() is DescriptionAttribute descriptionAttribute
            ? descriptionAttribute.Description : string.Empty;
    }
    
    public static TEnum? GetEnumByDescription<TEnum>(this string description)
        where TEnum : Enum
    {
        var type = typeof(TEnum);
        var values = Enum.GetValues(type);

        foreach (var value in values)
        {
            var memberInfo = type.GetMember(type.GetEnumName(value)!);
            if (memberInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute
                && string.Compare(description, descriptionAttribute.Description, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return (TEnum)value;
            }
        }
        return default;
    }
}