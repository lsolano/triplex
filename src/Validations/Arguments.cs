using System;
using System.Diagnostics;
using System.Linq;
using Triplex.Validations.Exceptions;
using Triplex.Validations.Utilities;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Triplex.Validations
{
    /// <summary>
    /// Utility class used to validate arguments. Useful to check constructor and public methods arguments.
    /// If checks are violated an instance of <see cref="System.ArgumentException" /> is thrown.
    /// </summary>
    public static class Arguments
    {
        #region Null and Empty Checks
        /// <summary>
        /// Checks that the provided value is not <see langword="null" />.
        /// </summary>
        /// <typeparam name="TParamType">Type of the value being checked.</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        [DebuggerStepThrough]
        public static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType value, [ValidatedNotNull] string paramName) where TParamType : class
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)));

        /// <summary>
        /// Checks that the provided value is not <see langword="null" />.
        /// </summary>
        /// <remarks>
        /// <para>
        /// * Certain derivatives from <see cref="System.ArgumentException" /> such as <see cref="System.ArgumentNullException" /> does not allow direct message setting.
        /// </para>
        /// <para>
        /// This one will use the provided message and used to build a final one.
        /// </para>
        /// </remarks>
        /// <typeparam name="TParamType">Type of the value being checked.</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <param name="customMessage">Custom exception error message</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        [DebuggerStepThrough]
        public static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage) where TParamType : class
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)), customMessage.ValueOrThrowIfNull(nameof(customMessage)));

        /// <summary>
        /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains whie-space only characteres.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <param name="customMessage">Custom exception error message</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        /// <exception cref="ArgumentFormatException">If <paramref name="value"/> contains only white-space characters</exception>
        public static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] string value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
            => NotNullOrEmpty(value, paramName, customMessage)
                    .ValueOrThrowIfWhiteSpaceOnly(paramName, customMessage);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        /// <exception cref="ArgumentFormatException">If <paramref name="value"/> contains only white-space characters</exception>
        public static string NotNullOrEmpty([ValidatedNotNull] string value, [ValidatedNotNull] string paramName)
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)))
                    .ValueOrThrowIfZeroLength<string>(paramName);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter's name, can not be <see langword="null" /></param>
        /// <param name="customMessage">Custom message, can not be <see langword="null" /></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword = "null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        public static string NotNullOrEmpty([ValidatedNotNull] string value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
            => value.ValueOrThrowIfNull(paramName.ValueOrThrowIfNull(nameof(paramName)), customMessage.ValueOrThrowIfNull(nameof(customMessage)))
                        .ValueOrThrowIfZeroLength<string>(paramName, customMessage);

        #endregion

        #region Enumerations Checks

        /// <summary>
        /// Checks that the actual value is a valid enumeration constant.
        /// </summary>
        /// <typeparam name="TEnumType">Enumeration type</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="value"/> is not within <typeparamref name="TEnumType"/></exception>
        [DebuggerStepThrough]
        public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName) where TEnumType : Enum
            => Enum.IsDefined(typeof(TEnumType), value) ? value : ThrowArgumentOutOfRangeExceptionForEnum(value, paramName, null);

        /// <summary>
        /// Checks that the actual value is a valid enumeration constant.
        /// </summary>
        /// <typeparam name="TEnumType">Enumeration type</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="customMessage">Custom error message</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="value"/> is not within <typeparamref name="TEnumType"/></exception>
        [DebuggerStepThrough]
        public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName, string customMessage) where TEnumType : Enum
            => Enum.IsDefined(typeof(TEnumType), value) ? value : ThrowArgumentOutOfRangeExceptionForEnum(value, paramName, customMessage);

        private static TEnumType ThrowArgumentOutOfRangeExceptionForEnum<TEnumType>(TEnumType value, string paramName, string? customMessage) where TEnumType : Enum
        {
            const string valueNotWithinEnumMessageTemplate = "Value is not a member of enum {enumType}.";

            string finalMessage = customMessage
                                  ?? valueNotWithinEnumMessageTemplate.Replace("{enumType}", typeof(TEnumType).Name
                                      #if NETSTANDARD || NETCOREAPP
                                      ,StringComparison.Ordinal
                                      #endif
                                      );

            throw new ArgumentOutOfRangeException(paramName, value, finalMessage);
        }

        #endregion


        #region Out-of-range Checks

        /// <summary>
        /// Checks that the given <paramref name="value"/> is less than <paramref name="other"/>. This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not less than <paramref name="other"/></exception>
        public static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable value,
            [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName)
            where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, null);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is less than <paramref name="other"/>. This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not less than <paramref name="other"/></exception>
        [DebuggerStepThrough]
        public static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName,
            [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            NotNull(customMessage, nameof(customMessage));

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, customMessage);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is less than or equal to <paramref name="other"/>.
        /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not less than or equal to <paramref name="other"/></exception>
        public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value,
            [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName)
            where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, null);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is less than or equal to <paramref name="other"/>.
        /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not less than or equal to <paramref name="other"/></exception>
        public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value,
            [ValidatedNotNull] TComparable other, string paramName, [ValidatedNotNull] string customMessage)
            where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            NotNull(customMessage, nameof(customMessage));

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, customMessage);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is greater than <paramref name="other"/>.
        /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not greater than <paramref name="other"/></exception>
        [DebuggerStepThrough]
        public static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName) where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, null);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is greater than <paramref name="other"/>.
        /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not greater than <paramref name="other"/></exception>
        [DebuggerStepThrough]
        public static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName,
            [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            NotNull(customMessage, nameof(customMessage));

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, customMessage);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is greater than or equal to <paramref name="other"/>.
        /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not greater than or equal to <paramref name="other"/></exception>
        [DebuggerStepThrough]
        public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName) where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, null);
        }

        /// <summary>
        /// Checks that the given <paramref name="value"/> is greater than or equal to <paramref name="other"/>.
        /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
        /// </summary>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="other">Comparison target, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not greater than or equal to <paramref name="other"/></exception>
        [DebuggerStepThrough]
        public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName,
            [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            NotNull(customMessage, nameof(customMessage));

            ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(SimpleOption.SomeNotNull(other));
            return CheckBoundaries(value, range, paramName, customMessage);
        }

        private static TComparable CheckBoundaries<TComparable>(
            [ValidatedNotNull] TComparable value,
            ComparableRange<TComparable> range,
            [ValidatedNotNull] string paramName,
            string? customMessage) where TComparable : IComparable<TComparable>
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            NotNull(paramName, nameof(paramName));

            return range.IsWithin(value, paramName, customMessage);
        }

        #endregion


        #region Extionsion methods
        private static T ValueOrThrowIfNull<T>(this T value, string paramName)
        {
            if (value != null)
            {
                return value;
            }

            throw new ArgumentNullException(paramName);
        }

        private static T ValueOrThrowIfNull<T>(this T value, string paramName, string customMessage)
        {
            if (value != null)
            {
                return value;
            }

            throw new ArgumentNullException(paramName, customMessage);
        }

        private static string ValueOrThrowIfZeroLength<T>(this string value, string paramName)
        {
            if (value.Length != 0)
            {
                return value;
            }

            throw new ArgumentOutOfRangeException(paramName, value.Length, "String can not be empty (zero length).");
        }

        private static string ValueOrThrowIfZeroLength<T>(this string value, string paramName, string customMessage)
        {
            if (value.Length != 0)
            {
                return value;
            }

            throw new ArgumentOutOfRangeException(paramName, value.Length, customMessage);
        }

        private static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName)
        {
            return ValueOrThrowIfWhiteSpaceOnly(value, paramName, "Can not be white-space only.");
        }

        private static string ValueOrThrowIfWhiteSpaceOnly(this string value, string paramName, string customMessage)
        {
            if (value.Any(ch => ch.IsNotWhiteSpace()))
            {
                return value;
            }

            throw new ArgumentFormatException(paramName: paramName, message: customMessage);
        }

        private static bool IsNotWhiteSpace(this char ch) => !char.IsWhiteSpace(ch);

        #endregion
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
