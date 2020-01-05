using System;
using System.Linq;
using Triplex.Validations.Exceptions;

namespace Triplex.Validations.ArgumentsHelpers
{
#pragma warning disable CA1303 // Do not pass literals as localized parameters
    internal static class Extensions
    {
        internal static T ValueOrThrowIfNull<T>(this T value, string paramName)
        {
            if (value != null)
            {
                return value;
            }

            throw new ArgumentNullException(paramName);
        }

        internal static T ValueOrThrowIfNull<T>(this T value, string paramName, string customMessage)
        {
            if (value != null)
            {
                return value;
            }

            throw new ArgumentNullException(paramName, customMessage);
        }

        internal static string ValueOrThrowIfZeroLength<T>(this string value, string paramName)
        {
            if (value.Length != 0)
            {
                return value;
            }

            throw new ArgumentOutOfRangeException(paramName, value.Length, "String can not be empty (zero length).");
        }

        internal static string ValueOrThrowIfZeroLength<T>(this string value, string paramName, string customMessage)
        {
            if (value.Length != 0)
            {
                return value;
            }

            throw new ArgumentOutOfRangeException(paramName, value.Length, customMessage);
        }

        internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName)
        {
            return ValueOrThrowIfWhiteSpaceOnly(value, paramName, "Can not be white-space only.");
        }

        internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName, string customMessage)
        {
            if (value.Any(ch => ch.IsNotWhiteSpace()))
            {
                return value;
            }

            throw new ArgumentFormatException(paramName: paramName, message: customMessage);
        }

        internal static bool IsNotWhiteSpace(this char ch) => !char.IsWhiteSpace(ch);

        internal static TEnumType ValueOrThrowIfNotDefined<TEnumType>(this TEnumType value, string paramName) where TEnumType : Enum
            => ValueOrThrowIfNotDefined(value, paramName, null);

        internal static TEnumType ValueOrThrowIfNotDefined<TEnumType>(this TEnumType value, string paramName, string? customMessage) where TEnumType : Enum
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
    }
#pragma warning restore CA1303 // Do not pass literals as localized parameters
}
