namespace Triplex.Validations.ArgumentsHelpers;

internal static class NullAndEmptyChecks
{
    [return: NotNull]
    internal static TParamType NotNull<TParamType>([NotNull, ValidatedNotNull] TParamType? value,
        [NotNull, ValidatedNotNull] string paramName) where TParamType : class
        => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)));

    [return: NotNull]
    internal static TParamType NotNull<TParamType>([NotNull, ValidatedNotNull] TParamType? value,
        [NotNull, ValidatedNotNull] string paramName, [NotNull, ValidatedNotNull] string customMessage)
            where TParamType : class
        => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)),
            customMessage.ValueOrThrowIfNull(nameof(customMessage)));

    [return: NotNull]
    internal static string NotNullEmptyOrWhiteSpaceOnly([NotNull, ValidatedNotNull] string? value,
        [NotNull, ValidatedNotNull] string paramName)
        => NotNullOrEmpty(value, paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)))
                .ValueOrThrowIfWhiteSpaceOnly(paramName);

    [return: NotNull]
    internal static string NotNullEmptyOrWhiteSpaceOnly([NotNull, ValidatedNotNull] string? value,
        [NotNull, ValidatedNotNull] string paramName, [NotNull, ValidatedNotNull] string customMessage)
        => NotNullOrEmpty(value, paramName, customMessage)
                .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

    [return: NotNull]
    internal static string NotNullOrEmpty([NotNull, ValidatedNotNull] string? value,
        [NotNull, ValidatedNotNull] string paramName)
        => value.ValueOrThrowIfNullOrZeroLength(
            paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)));

    [return: NotNull]
    internal static string NotNullOrEmpty([NotNull, ValidatedNotNull] string? value,
        [NotNull, ValidatedNotNull] string paramName, [NotNull, ValidatedNotNull] string customMessage)
        => value.ValueOrThrowIfNullOrZeroLength(
            paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
            customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));
}
