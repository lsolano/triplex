using Triplex.Validations.Utilities;

namespace Triplex.Validations.ArgumentsHelpers
{
    internal static class NullAndEmptyChecks
    {
        internal static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType value, [ValidatedNotNull] string paramName) where TParamType : class
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)));

        internal static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage) where TParamType : class
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)), customMessage.ValueOrThrowIfNull(nameof(customMessage)));

        internal static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] string value, [ValidatedNotNull] string paramName)
            => NotNullOrEmpty(value, paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)))
                    .ValueOrThrowIfWhiteSpaceOnly(paramName);

        internal static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] string value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
            => NotNullOrEmpty(value, paramName, customMessage)
                    .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

        internal static string NotNullOrEmpty([ValidatedNotNull] string value, [ValidatedNotNull] string paramName)
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)).ValueOrThrowIfZeroLength(nameof(paramName)))
                    .ValueOrThrowIfZeroLength(paramName);

        internal static string NotNullOrEmpty([ValidatedNotNull] string value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
            => value.ValueOrThrowIfNullOrZeroLength(
                paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
                customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));
    }
}
