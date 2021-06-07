using Triplex.Validations.Utilities;

namespace Triplex.Validations.ArgumentsHelpers
{
    internal static class NullAndEmptyChecks
    {
        internal static TParamType NotNull<TParamType>([ValidatedNotNull] in TParamType? value, [ValidatedNotNull] in string paramName) where TParamType : class
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)));

        internal static TParamType NotNull<TParamType>([ValidatedNotNull] in TParamType? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage) where TParamType : class
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)), customMessage.ValueOrThrowIfNull(nameof(customMessage)));

        internal static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName)
            => NotNullOrEmpty(value, paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)))
                    .ValueOrThrowIfWhiteSpaceOnly(paramName);

        internal static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
            => NotNullOrEmpty(value, paramName, customMessage)
                    .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

        internal static string NotNullOrEmpty([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName)
            => value.ValueOrThrowIfNullOrZeroLength(
                paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)));

        internal static string NotNullOrEmpty([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
            => value.ValueOrThrowIfNullOrZeroLength(
                paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
                customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));
    }
}
