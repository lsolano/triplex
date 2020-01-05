using System;
using System.Diagnostics;
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
        public static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType value, [ValidatedNotNull] string paramName) where TParamType : class
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
        public static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage) where TParamType : class
            => NullAndEmptyChecks.NotNull(value, paramName, customMessage);

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
        [DebuggerStepThrough]
        public static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] string value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
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
        public static string NotNullOrEmpty([ValidatedNotNull] string value, [ValidatedNotNull] string paramName)
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
        public static string NotNullOrEmpty([ValidatedNotNull] string value, [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
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
        public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName) where TEnumType : Enum
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
        public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName, string customMessage) where TEnumType : Enum
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
        public static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName)
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
        public static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName,
            [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
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
        public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value,
            [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName)
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
        public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value,
            [ValidatedNotNull] TComparable other, string paramName, [ValidatedNotNull] string customMessage)
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
        public static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName)
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
        public static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName,
            [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
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
        public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable value, [ValidatedNotNull] TComparable other, [ValidatedNotNull] string paramName)
            where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.GreaterThanOrEqualTo(value, other, paramName);

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
            => OutOfRangeChecks.GreaterThanOrEqualTo(value, other, paramName, customMessage);

        #endregion
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
