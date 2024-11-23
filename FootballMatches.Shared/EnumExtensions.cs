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
}