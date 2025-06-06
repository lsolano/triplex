namespace Triplex.Validations.ArgumentsHelpers;

internal static class NullAndEmptyChecks
{
    [return: NotNull]
    internal static TParamType NotNull<TParamType>([NotNull] TParamType? value,
        [NotNull] string paramName) where TParamType : class
        => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull());

    [return: NotNull]
    internal static TParamType NotNull<TParamType>([NotNull] TParamType? value,
        [NotNull] string paramName, [NotNull] string customMessage)
            where TParamType : class
        => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(),
            customMessage.ValueOrThrowIfNull());

    [return: NotNull]
    internal static string NotNullEmptyOrWhiteSpaceOnly([NotNull] string? value,
        [NotNull] string paramName)
        => NotNullOrEmpty(value, paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly())
                .ValueOrThrowIfWhiteSpaceOnly(paramName);

    [return: NotNull]
    internal static string NotNullEmptyOrWhiteSpaceOnly([NotNull] string? value,
        [NotNull] string paramName, [NotNull] string customMessage)
        => NotNullOrEmpty(value, paramName, customMessage)
                .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

    [return: NotNull]
    internal static string NotNullOrEmpty([NotNull] string? value,
        [NotNull] string paramName)
        => value.ValueOrThrowIfNullOrZeroLength(
            paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly());

    [return: NotNull]
    internal static string NotNullOrEmpty([NotNull] string? value,
        [NotNull] string paramName, [NotNull] string customMessage)
        => value.ValueOrThrowIfNullOrZeroLength(
            paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(),
            customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly());
}
