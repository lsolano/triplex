using System.Runtime.CompilerServices;

namespace Triplex.Validations.ArgumentsHelpers;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
internal static class Extensions
{
    [return: NotNull]
    internal static T ValueOrThrowIfNull<T>([NotNull] this T? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        => value ?? throw new ArgumentNullException(paramName);

    [return: NotNull]
    internal static T ValueOrThrowIfNull<T>([NotNull] this T? value, string paramName, string customMessage)
        => value ?? throw new ArgumentNullException(paramName, customMessage);

    [return: NotNull]
    internal static T ValueOrThrowInvalidOperationIfNull<T>([NotNull] this T? stateElement,
        string elementName)
            => stateElement
                ?? throw new InvalidOperationException($"Operation not allowed when {elementName} is null.");

    [return: NotNull]
    internal static string ValueOrThrowIfZeroLength(this string value, string paramName)
        => ValueOrThrowIfZeroLength(value, paramName, "Can not be empty (zero length).");

    [return: NotNull]
    internal static string ValueOrThrowIfZeroLength(this string value, string paramName, string customMessage)
        => value.Length is not 0
            ? value
            : throw new ArgumentFormatException(paramName: paramName, message: customMessage);

    [return: NotNull]
    internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName)
        => ValueOrThrowIfWhiteSpaceOnly(value, paramName, "Can not be white-space only.");

    [return: NotNull]
    internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName, string customMessage)
        => value.Any(ch => ch.IsNotWhiteSpace())
            ? value
            : throw new ArgumentFormatException(paramName: paramName, message: customMessage);

    [return: NotNull]
    internal static string ValueOrThrowIfNullOrZeroLength([NotNull] this string? value,
        string paramName)
        => ValueOrThrowIfNull(value, paramName)
            .ValueOrThrowIfZeroLength(paramName);

    [return: NotNull]
    internal static string ValueOrThrowIfNullOrZeroLength([NotNull] this string? value,
        string paramName, string customMessage)
        => ValueOrThrowIfNull(value, paramName, customMessage)
            .ValueOrThrowIfZeroLength(paramName, customMessage);

    [return: NotNull]
    internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        => ValueOrThrowIfNull(value, paramName)
            .ValueOrThrowIfZeroLength(paramName)
                .ValueOrThrowIfWhiteSpaceOnly(paramName);

    [return: NotNull]
    internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly([NotNull] this string? value,
        string paramName, string customMessage)
        => ValueOrThrowIfNull(value, paramName, customMessage)
            .ValueOrThrowIfZeroLength(paramName, customMessage)
                .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

    internal static bool IsNotWhiteSpace(this char ch) => !char.IsWhiteSpace(ch);

    internal static TEnumType ValueOrThrowIfNotDefined<TEnumType>(this TEnumType value, string paramName)
        where TEnumType : Enum
        => ValueOrThrowIfNotDefined(value, paramName, null);

    internal static TEnumType ValueOrThrowIfNotDefined<TEnumType>(this TEnumType value, string paramName,
        string? customMessage) where TEnumType : Enum
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

    [return: NotNull]
    internal static TType[] ValueOrThrowIfNullOrWithLessThanElements<TType>(
        [NotNull] this TType[]? value, int minimumElements, string paramName)
    {
        OutOfRangeChecks.GreaterThanOrEqualTo(ValueOrThrowIfNull(value, paramName).Length, minimumElements, paramName);

        return value!;
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters