namespace Triplex.Validations.ArgumentsHelpers;

internal static class EnumerationChecks
{
    internal static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName)
        where TEnumType : Enum
        => value.ValueOrThrowIfNotDefined(paramName);

    internal static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName, string customMessage)
        where TEnumType : Enum
        => value.ValueOrThrowIfNotDefined(paramName, customMessage);
}
