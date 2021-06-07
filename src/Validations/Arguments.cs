using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

using Triplex.Validations.Algorithms.Checksum;
using Triplex.Validations.ArgumentsHelpers;
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
        public static TParamType NotNull<TParamType>([ValidatedNotNull] in TParamType? value, [ValidatedNotNull] in string paramName) where TParamType : class
            => NullAndEmptyChecks.NotNull(value, paramName);

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
        public static TParamType NotNull<TParamType>([ValidatedNotNull] in TParamType? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage) where TParamType : class
            => NullAndEmptyChecks.NotNull(value, paramName, customMessage);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains white-space only characters.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        /// <exception cref="ArgumentFormatException">If <paramref name="value"/> contains only white-space characters</exception>
        public static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName)
            => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains white-space only characters.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <param name="customMessage">Custom exception error message</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        /// <exception cref="ArgumentFormatException">If <paramref name="value"/> contains only white-space characters</exception>
        [DebuggerStepThrough]
        public static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
            => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName, customMessage);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        /// <exception cref="ArgumentFormatException">If <paramref name="value"/> contains only white-space characters</exception>
        [DebuggerStepThrough]
        public static string NotNullOrEmpty([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName)
            => NullAndEmptyChecks.NotNullOrEmpty(value, paramName);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter's name, can not be <see langword="null" /></param>
        /// <param name="customMessage">Custom message, can not be <see langword="null" /></param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentNullException">If any paramete is <see langword = "null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
        [DebuggerStepThrough]
        public static string NotNullOrEmpty([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
            => NullAndEmptyChecks.NotNullOrEmpty(value, paramName, customMessage);

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
        public static TEnumType ValidEnumerationMember<TEnumType>(in TEnumType value, in string paramName) where TEnumType : Enum
            => EnumerationChecks.ValidEnumerationMember(value, paramName);

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
        public static TEnumType ValidEnumerationMember<TEnumType>(in TEnumType value, in string paramName, in string customMessage) where TEnumType : Enum
            => EnumerationChecks.ValidEnumerationMember(value, paramName, customMessage);

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
        [DebuggerStepThrough]
        public static TComparable LessThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName)
            where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.LessThan(value, other, paramName);

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
        public static TComparable LessThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName,
            [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.LessThan(value, other, paramName, customMessage);

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
        [DebuggerStepThrough]
        public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value,
            [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName)
            where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.LessThanOrEqualTo(value, other, paramName);

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
        [DebuggerStepThrough]
        public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value,
            [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
            where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.LessThanOrEqualTo(value, other, paramName, customMessage);

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
        public static TComparable GreaterThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName)
            where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.GreaterThan(value, other, paramName);

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
        public static TComparable GreaterThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName,
            [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.GreaterThan(value, other, paramName, customMessage);

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
        public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName)
            where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.GreaterThanOrEqualTo(value, other, paramName);

        /// <summary>
        /// <p>Checks that the given <paramref name="value"/> is greater than or equal to <paramref name="other"/>.</p>
        /// <p>This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.</p>
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
        public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName,
            [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.GreaterThanOrEqualTo(value, other, paramName, customMessage);

        /// <summary>
        /// <p>Checks that the given <paramref name="value"/> is between [<paramref name="fromInclusive"/>, <paramref name="toInclusive"/>] (closed range).</p>
        /// <p>This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.</p>
        /// </summary>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="fromInclusive">Lower bound, can not be <see langword="null"/></param>
        /// <param name="toInclusive">Upper bound, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <typeparam name="TComparable"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not between [<paramref name="fromInclusive"/>, <paramref name="toInclusive"/>]</exception>
        [DebuggerStepThrough]
        public static TComparable Between<TComparable>(
            [ValidatedNotNull] in TComparable? value,
            [ValidatedNotNull] in TComparable? fromInclusive,
            [ValidatedNotNull] in TComparable? toInclusive,
            [ValidatedNotNull] in string paramName,
            [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
                => OutOfRangeChecks.Between(value, fromInclusive, toInclusive, paramName, customMessage);

        #endregion

        #region Checksum algorithms

        private static Regex LuhnDigitsRegex = new Regex("[0-9]{2}", RegexOptions.Compiled);

        /// <summary>
        /// Validates that the given argument (<paramref name="value" />) has a valid checksum digit as described by the Luhn algorithm or Luhn formula.
        /// </summary>
        /// <remarks>
        /// See https://en.wikipedia.org/wiki/Luhn_algorithm and https://www.investopedia.com/terms/l/luhn-algorithm.asp
        /// </remarks>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="FormatException">If <paramref name="value"/> is not valid as described by the Luhn algorithm.</exception>
        [DebuggerStepThrough]
        public static string ValidLuhnChecksum([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
        {
            (string validParamName, string validCustomMessage) =
                (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
                 customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));

            string notNullValue = NullAndEmptyChecks.NotNull(value, validParamName);

            ValidateLuhnSequenceFormat(notNullValue, validCustomMessage);

            return LuhnFormula.IsValid(ToDigitsArray(notNullValue)) ? notNullValue : throw new FormatException(validCustomMessage);
        }

        private static void ValidateLuhnSequenceFormat(in string notNullValue, in string validCustomMessage)
        {
            bool hasInvalidFormat = !LuhnDigitsRegex.IsMatch(notNullValue);
            if (hasInvalidFormat)
            {
                throw new FormatException(validCustomMessage);
            }
        }
        
        private static int[] ToDigitsArray(string notNullValue)
        {
            const int zeroAsciiCode = '0';
            return notNullValue.Select(ch => ch - zeroAsciiCode).ToArray();
        }

        #endregion

        #region Known Encodings

        /// <summary>
        /// Validates that the given argument (<paramref name="value" />) is a valid Base64 String.
        /// </summary>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <returns><paramref name="value" /> or an exception</returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="FormatException">If <paramref name="value"/> is not a valid Base64 String.</exception>
        [DebuggerStepThrough]
        public static string ValidBase64([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName) {
            string validParamName =
                paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName));

            string notNullValue = value.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(validParamName);
            
            return IsBase64String(notNullValue) ? notNullValue : throw new FormatException($"{validParamName} is not a valid Base64 String.");
        }

        /// <summary>
        /// Validates that the given argument (<paramref name="value" />) is a valid Base64 String.
        /// </summary>
        /// <param name="value">Value to check, can not be <see langword="null"/></param>
        /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
        /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
        /// <returns><paramref name="value" /> or an exception</returns>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
        /// <exception cref="FormatException">If <paramref name="value"/> is not a valid Base64 String.</exception>
        [DebuggerStepThrough]
        public static string ValidBase64([ValidatedNotNull] in string? value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage) {
            (string validParamName, string validCustomMessage) =
                (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
                 customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));
            
            string notNullValue = value.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(validParamName, validCustomMessage);

            return IsBase64String(notNullValue) ? notNullValue : throw new FormatException(validCustomMessage);
        }

        private static bool IsBase64String(in string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer , out int bytesParsed);
        }

        #endregion // Known Encodings

        #region Emptyness
        /// <summary>
        /// Checks that the provided value is not empty.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentException">If <paramref name="value"/> is an empty <see cref="Guid"/>.</exception>
        [DebuggerStepThrough]
        public static Guid NotEmpty(in Guid value, [ValidatedNotNull] in string paramName) {
            string validParamName = paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName));

            if (IsEmpty(value)) {
                throw new ArgumentException(paramName);
            }

            return value;
        } //NotEmptyGuidMessage

        /// <summary>
        /// Checks that the provided value is not empty.
        /// </summary>
        /// <remarks>
        /// <para>
        /// * Certain derivatives from <see cref="System.ArgumentException" /> such as <see cref="System.ArgumentNullException" /> does not allow direct message setting.
        /// </para>
        /// <para>
        /// This one will use the provided message and used to build a final one.
        /// </para>
        /// </remarks>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <param name="customMessage">Custom exception error message</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception cref="ArgumentException">If <paramref name="value"/> is an empty <see cref="Guid"/>.</exception>
        [DebuggerStepThrough]
        public static Guid NotEmpty(in Guid value, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage) 
        { 
            (string validParamName, string validCustomMessage) =
                (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
                 customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));

            if (IsEmpty(value)) {
                throw new ArgumentException(paramName: paramName, message: validCustomMessage);
            }

            return value;
        }

        private static bool IsEmpty(in Guid value) => value == default;

        #endregion //Emptyness

        #region General Purpose Checks

        /// <summary>
        /// Succeeds if <paramref name="precondition"/> (value or expression) is <see langword="true"/>.
        /// </summary>
        /// <param name="precondition">Boolean expression that must be <see langword="true"/> for the argument check to succeed.</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <param name="preconditionDescription">Description for the custom precondition.</param>
        public static void CompliesWith(in bool precondition, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string preconditionDescription) {
            (string validParamName, string validPreconditionDescription) =
                (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
                 preconditionDescription.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(preconditionDescription)));

            if (!precondition) {
                throw new ArgumentException(paramName: validParamName, message: validPreconditionDescription);
            }
        }
        
        #endregion //General Purpose Checks
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
