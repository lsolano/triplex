using System.Runtime.CompilerServices;

namespace Triplex.Validations.ArgumentsHelpers;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
internal static class Extensions
{
    [return: NotNull]
    internal static T Check<T>([NotNull] this T? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        => value ?? throw new ArgumentNullException(paramName);

    [return: NotNull]
    internal static T CheckWithParamName<T>([NotNull] this T? value, string paramName)
        => value ?? throw new ArgumentNullException(paramName);

    [return: NotNull]
    internal static T CheckWithParamName<T>([NotNull] this T? value, string paramName, string customMessage)
        => value ?? throw new ArgumentNullException(paramName, customMessage);

    [return: NotNull]
    internal static T CheckOrInvalidOperationException<T>([NotNull] this T? stateElement,
        string elementName)
            => stateElement
                ?? throw new InvalidOperationException($"Operation not allowed when {elementName} is null.");

    [return: NotNull]
    internal static string CheckNotZeroLength([NotNull] this string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
        => CheckWithParamName(value, paramName)
            .DoCheckNotZeroLength(paramName, "Can not be empty (zero length).");

    [return: NotNull]
    internal static string CheckNotZeroLength([NotNull] this string? value, string paramName, string customMessage)
        => CheckWithParamName(value, paramName, customMessage)
            .DoCheckNotZeroLength(paramName, customMessage);

    [return: NotNull]
    private static string DoCheckNotZeroLength(this string value, string paramName, string customMessage)
        => value.Length is not 0
            ? value
            : throw new ArgumentFormatException(paramName: paramName, message: customMessage);

    [return: NotNull]
    internal static string CheckNotWhiteSpaceOnly(this string value, string paramName)
        => CheckNotWhiteSpaceOnly(value, paramName, "Can not be white-space only.");

    [return: NotNull]
    internal static string CheckNotWhiteSpaceOnly(this string value, string paramName, string customMessage)
        => value.Any(ch => ch.IsNotWhiteSpace())
            ? value
            : throw new ArgumentFormatException(paramName: paramName, message: customMessage);

    [return: NotNull]
    internal static string CheckNotZeroLengthOrWhiteSpaceOnly(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        => Check(value, paramName)
            .CheckNotZeroLength(paramName)
                .CheckNotWhiteSpaceOnly(paramName);

    [return: NotNull]
    internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly([NotNull] this string? value,
        string paramName, string customMessage)
        => CheckWithParamName(value, paramName, customMessage)
            .DoCheckNotZeroLength(paramName, customMessage)
                .CheckNotWhiteSpaceOnly(paramName, customMessage);

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
        _ = OutOfRangeChecks.GreaterThanOrEqualTo(Check(value, paramName).Length, minimumElements, paramName);

        return value!;
    }

    #region ParameterName and Exception Messages Helpers

    [return: NotNull]
    internal static string CheckParamName(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        => CheckWithParamName(value, paramName)
            .CheckNotZeroLength(paramName)
                .CheckNotWhiteSpaceOnly(paramName);
    
    [return: NotNull]
    internal static string CheckExceptionMessage(
        [NotNull] this string? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        => CheckWithParamName(value, paramName)
            .CheckNotZeroLength(paramName)
                .CheckNotWhiteSpaceOnly(paramName);

    #endregion
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters