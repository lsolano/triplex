using Triplex.Validations.Exceptions;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.ArgumentsHelpers;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
internal static class Extensions
{
    internal static T ValueOrThrowIfNull<T>([ValidatedNotNull] this T? value, in string paramName)
    {
        if (value is not null)
        {
            return value;
        }

        throw new ArgumentNullException(paramName);
    }

    internal static T ValueOrThrowIfNull<T>([ValidatedNotNull] this T? value, in string paramName, in string customMessage)
    {
        if (value is not null)
        {
            return value;
        }

        throw new ArgumentNullException(paramName, customMessage);
    }

    internal static string ValueOrThrowIfZeroLength(this string value, in string paramName)
        => ValueOrThrowIfZeroLength(value, paramName, "Can not be empty (zero length).");

    internal static string ValueOrThrowIfZeroLength(this string value, in string paramName, in string customMessage)
    {
        if (value.Length is not 0)
        {
            return value;
        }

        throw new ArgumentOutOfRangeException(paramName, value.Length, customMessage);
    }

    internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, in string paramName)
        => ValueOrThrowIfWhiteSpaceOnly(value, paramName, "Can not be white-space only.");

    internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, in string paramName, in string customMessage)
    {
        if (value.Any(ch => ch.IsNotWhiteSpace()))
        {
            return value;
        }

        throw new ArgumentFormatException(paramName: paramName, message: customMessage);
    }

    internal static string ValueOrThrowIfNullOrZeroLength([ValidatedNotNull] this string? value, in string paramName)
        => ValueOrThrowIfNull(value, paramName)
            .ValueOrThrowIfZeroLength(paramName);

    internal static string ValueOrThrowIfNullOrZeroLength([ValidatedNotNull] this string? value, in string paramName, in string customMessage)
        => ValueOrThrowIfNull(value, paramName, customMessage)
            .ValueOrThrowIfZeroLength(paramName, customMessage);

    internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly([ValidatedNotNull] this string? value, in string paramName)
        => ValueOrThrowIfNull(value, paramName)
            .ValueOrThrowIfZeroLength(paramName)
                .ValueOrThrowIfWhiteSpaceOnly(paramName);

    internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly([ValidatedNotNull] this string? value, in string paramName, in string customMessage)
        => ValueOrThrowIfNull(value, paramName, customMessage)
            .ValueOrThrowIfZeroLength(paramName, customMessage)
                .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

    internal static bool IsNotWhiteSpace(this char ch) => !char.IsWhiteSpace(ch);

    internal static TEnumType ValueOrThrowIfNotDefined<TEnumType>(this TEnumType value, in string paramName) where TEnumType : Enum
        => ValueOrThrowIfNotDefined(value, paramName, null);

    internal static TEnumType ValueOrThrowIfNotDefined<TEnumType>(this TEnumType value, in string paramName, in string? customMessage) where TEnumType : Enum
    {
        if (Enum.IsDefined(typeof(TEnumType), value))
        {
            return value;
        }

        const string valueNotWithinEnumMessageTemplate = "Value is not a member of enum {enumType}.";

        string finalMessage = customMessage
                              ?? valueNotWithinEnumMessageTemplate.Replace("{enumType}", typeof(TEnumType).Name
#if NETSTANDARD || NETCOREAPP
                                      , StringComparison.Ordinal
#endif
                                      );

        throw new ArgumentOutOfRangeException(paramName, value, finalMessage);
    }

    internal static TType[] ValueOrThrowIfNullOrWithLessThanElements<TType>([ValidatedNotNull] this TType[]? value, in int minimumElements, in string paramName)
    {
        OutOfRangeChecks.GreaterThanOrEqualTo(ValueOrThrowIfNull(value, paramName).Length, minimumElements, paramName);

        return value!;
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
