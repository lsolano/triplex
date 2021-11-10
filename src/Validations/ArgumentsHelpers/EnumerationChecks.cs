namespace Triplex.Validations.ArgumentsHelpers
{
    internal static class EnumerationChecks
    {
        internal static TEnumType ValidEnumerationMember<TEnumType>(in TEnumType value, in string paramName) where TEnumType : Enum
            => value.ValueOrThrowIfNotDefined(paramName);

        internal static TEnumType ValidEnumerationMember<TEnumType>(in TEnumType value, in string paramName, in string customMessage) where TEnumType : Enum
            => value.ValueOrThrowIfNotDefined(paramName, customMessage);
    }
}
