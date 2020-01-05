using System;
using System.Linq;
using Triplex.Validations.Exceptions;
using System.CodeDom.Compiler;

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

        internal static string ValueOrThrowIfZeroLength(this string value, string paramName)
            => ValueOrThrowIfZeroLength(value, paramName, "Can not be empty (zero length).");

        internal static string ValueOrThrowIfZeroLength(this string value, string paramName, string customMessage)
        {
            if (value.Length != 0)
            {
                return value;
            }

            throw new ArgumentOutOfRangeException(paramName, value.Length, customMessage);
        }

        internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName)
            => ValueOrThrowIfWhiteSpaceOnly(value, paramName, "Can not be white-space only.");

        internal static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName, string customMessage)
        {
            if (value.Any(ch => ch.IsNotWhiteSpace()))
            {
                return value;
            }

            throw new ArgumentFormatException(paramName: paramName, message: customMessage);
        }

        internal static string ValueOrThrowIfNullOrZeroLength(this string value, string paramName)
            => ValueOrThrowIfNull(value, paramName)
                .ValueOrThrowIfZeroLength(paramName);

        internal static string ValueOrThrowIfNullOrZeroLength(this string value, string paramName, string customMessage)
            => ValueOrThrowIfNull(value, paramName, customMessage)
                .ValueOrThrowIfZeroLength(paramName, customMessage);

        internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(this string value, string paramName)
            => ValueOrThrowIfNull(value, paramName)
                .ValueOrThrowIfZeroLength(paramName)
                    .ValueOrThrowIfWhiteSpaceOnly(paramName);

        internal static string ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(this string value, string paramName, string customMessage)
            => ValueOrThrowIfNull(value, paramName, customMessage)
                .ValueOrThrowIfZeroLength(paramName, customMessage)
                    .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

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

        internal static StringParameterValidator CheckFor(this string value, string paramName) => StringParameterValidator.For(value, paramName);
    }

    internal class StringParameterValidator
    {
        private readonly string _value;
        private readonly string _paramName;

        public StringParameterValidator(string value, string paramName)
        {
            _value = value;
            _paramName = paramName;
        }

        internal static StringParameterValidator For(string value, string paramName)
        {
            return new StringParameterValidator(value, paramName);
        }

        internal string End() {
            return _value;
        }

        internal StringParameterValidator NotNull()
            => _value == null ? throw new ArgumentNullException(_paramName) : this;

        internal StringParameterValidator NotZeroLength() {
            return _value.Length == 0 ? throw new ArgumentOutOfRangeException(_paramName, _value.Length, "String can not be empty (zero length).") : this;
        }

        internal StringParameterValidator NotWhiteSpaceOnly() {
            if (_value.Any(ch => ch.IsNotWhiteSpace()))
            {
                return this;
            }

            throw new ArgumentFormatException(paramName: _paramName, message: "Can not be white-space only.");
        }
    }
#pragma warning restore CA1303 // Do not pass literals as localized parameters
}
