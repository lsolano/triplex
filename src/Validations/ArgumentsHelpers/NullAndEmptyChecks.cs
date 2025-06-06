namespace Triplex.Validations.ArgumentsHelpers;

/// <summary>
/// Provides methods to validate that parameters are not null, empty, or whitespace.
/// These methods throw exceptions if the checks fail, ensuring that the parameters meet the expected criteria.
/// This class is intended for internal use within the Triplex library and should not be used directly by consumers.
/// </summary>
/// <remarks>
/// As a general design principle, all checks begin with a <see langword="null"/> check.
/// </remarks>
internal static class NullAndEmptyChecks
{
    [return: NotNull]
    internal static TParamType Check<TParamType>([NotNull] TParamType? value, [NotNull] string paramName)
        where TParamType : class
        => value.CheckWithParamName(paramName.CheckParamName());

    [return: NotNull]
    internal static TParamType Check<TParamType>([NotNull] TParamType? value, [NotNull] string paramName, [NotNull] string customMessage)
            where TParamType : class
        => value.CheckWithParamName(paramName.CheckParamName(), customMessage.CheckExceptionMessage());

    [return: NotNull]
    internal static string NotNullEmptyOrWhiteSpaceOnly([NotNull] string? value, [NotNull] string paramName)
        => NotNullOrEmpty(value, paramName.CheckParamName())
                .CheckNotWhiteSpaceOnly(paramName);

    [return: NotNull]
    internal static string NotNullEmptyOrWhiteSpaceOnly([NotNull] string? value,
        [NotNull] string paramName, [NotNull] string customMessage)
        => NotNullOrEmpty(value, paramName, customMessage)
                .CheckNotWhiteSpaceOnly(paramName, customMessage);

    [return: NotNull]
    internal static string NotNullOrEmpty([NotNull] string? value, [NotNull] string paramName)
        => value.CheckNotZeroLength(
            paramName.CheckParamName());

    [return: NotNull]
    internal static string NotNullOrEmpty([NotNull] string? value,
        [NotNull] string paramName, [NotNull] string customMessage)
        => value.CheckNotZeroLength(
            paramName.CheckParamName(),
            customMessage.CheckExceptionMessage());
}
