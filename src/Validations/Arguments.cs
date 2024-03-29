﻿using Triplex.Validations.Algorithms.Checksum;
using Triplex.Validations.ArgumentsHelpers;
using Triplex.Validations.Exceptions;
using Triplex.Validations.Utilities;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Triplex.Validations;

/// <summary>
/// Utility class used to validate arguments. Useful to check constructor and public methods arguments.
/// If checks are violated an instance of <see cref="ArgumentException" /> is thrown.
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
    public static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType? value,
        [ValidatedNotNull] string paramName) where TParamType : class
        => NullAndEmptyChecks.NotNull(value, paramName);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" />.
    /// </summary>
    /// <remarks>
    /// <para>
    /// * Certain derivatives from <see cref="ArgumentException" /> such as <see cref="ArgumentNullException" />
    /// does not allow direct message setting.
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
    public static TParamType NotNull<TParamType>([ValidatedNotNull] TParamType? value,
        [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage) where TParamType : class
        => NullAndEmptyChecks.NotNull(value, paramName, customMessage);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains white-space 
    /// only characters.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    /// <exception cref="ArgumentFormatException">
    /// If <paramref name="value"/> contains only white-space characters
    /// </exception>
    [Obsolete("Please stop using this method, it will be removed on mayor release 4.x. Use NotEmptyOrWhiteSpaceOnly(string?, string) instead.", error: false)]
    [DebuggerStepThrough]
    public static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] string? value,
        [ValidatedNotNull] string paramName)
        => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains white-space 
    /// only characters.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <param name="customMessage">Custom exception error message</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    /// <exception cref="ArgumentFormatException">
    /// If <paramref name="value"/> contains only white-space characters
    /// </exception>
    [Obsolete("Please stop using this method, it will be removed on mayor release 4.x. Use NotEmptyOrWhiteSpaceOnly(string?, string, string) instead.", error: false)]
    [DebuggerStepThrough]
    public static string NotNullEmptyOrWhiteSpaceOnly([ValidatedNotNull] string? value,
        [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
        => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName, customMessage);

    /// <inheritdoc cref="NotNullEmptyOrWhiteSpaceOnly(string?, string)"/>
    [DebuggerStepThrough]
    public static string NotEmptyOrWhiteSpaceOnly([ValidatedNotNull] string? value,
        [ValidatedNotNull] string paramName)
        => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName);

    /// <inheritdoc cref="NotNullEmptyOrWhiteSpaceOnly(string?, string, string)"/>
    [DebuggerStepThrough]
    public static string NotEmptyOrWhiteSpaceOnly([ValidatedNotNull] string? value,
        [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
        => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName, customMessage);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any paramete is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    /// <exception cref="ArgumentFormatException">
    /// If <paramref name="value"/> contains only white-space characters
    /// </exception>
    [Obsolete("Please stop using this method, it will be removed on mayor release 4.x. Use NotEmpty(string?, string) instead.", error: false)]
    [DebuggerStepThrough]
    public static string NotNullOrEmpty([ValidatedNotNull] string? value, [ValidatedNotNull] string paramName)
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
    [Obsolete("Please stop using this method, it will be removed on mayor release 4.x. Use NotEmpty(string?, string, string) instead.", error: false)]
    [DebuggerStepThrough]
    public static string NotNullOrEmpty([ValidatedNotNull] string? value, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage)
        => NullAndEmptyChecks.NotNullOrEmpty(value, paramName, customMessage);

    /// <inheritdoc cref="NotNullOrEmpty(string?, string)"/>
    [DebuggerStepThrough]
    public static string NotEmpty([ValidatedNotNull] string? value, [ValidatedNotNull] string paramName)
        => NullAndEmptyChecks.NotNullOrEmpty(value, paramName);

    /// <inheritdoc cref="NotNullOrEmpty(string?, string, string)"/>
    [DebuggerStepThrough]
    public static string NotEmpty([ValidatedNotNull] string? value, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage)
        => NullAndEmptyChecks.NotNullOrEmpty(value, paramName, customMessage);

    #endregion

    #region Emptyness
    /// <summary>
    /// Checks that the provided value is not empty.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is an empty <see cref="Guid"/>.</exception>
    [DebuggerStepThrough]
    public static Guid NotEmpty(Guid value, [ValidatedNotNull] string paramName)
    {
        string validParamName = paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName));

        if (IsEmpty(value))
        {
            throw new ArgumentException(validParamName);
        }

        return value;
    }

    /// <summary>
    /// Checks that the provided value is not empty.
    /// </summary>
    /// <remarks>
    /// <para>
    /// * Certain derivatives from <see cref="ArgumentException" /> such as <see cref="ArgumentNullException" /> 
    /// does not allow direct message setting.
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
    public static Guid NotEmpty(Guid value, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage)
    {
        (string validParamName, string validCustomMessage) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
             customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));

        if (IsEmpty(value))
        {
            throw new ArgumentException(paramName: validParamName, message: validCustomMessage);
        }

        return value;
    }

    private static bool IsEmpty(Guid value) => value == default;

    #endregion //Emptyness

    #region Enumerations Checks

    /// <summary>
    /// Checks that the actual value is a valid enumeration constant.
    /// </summary>
    /// <typeparam name="TEnumType">Enumeration type</typeparam>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// When <paramref name="value"/> is not within <typeparamref name="TEnumType"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName)
        where TEnumType : Enum
        => EnumerationChecks.ValidEnumerationMember(value, paramName);

    /// <summary>
    /// Checks that the actual value is a valid enumeration constant.
    /// </summary>
    /// <typeparam name="TEnumType">Enumeration type</typeparam>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name</param>
    /// <param name="customMessage">Custom error message</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// When <paramref name="value"/> is not within <typeparamref name="TEnumType"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName,
        string customMessage) where TEnumType : Enum
        => EnumerationChecks.ValidEnumerationMember(value, paramName, customMessage);

    #endregion

    #region Out-of-range Checks

    /// <summary>
    /// Checks that the given <paramref name="value"/> is less than <paramref name="other"/>. 
    /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    /// <param name="value">Value to check, can not be <see langword="null"/></param>
    /// <param name="other">Comparison target, can not be <see langword="null"/></param>
    /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not less than <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
        => OutOfRangeChecks.LessThan(value, other, paramName);

    /// <summary>
    /// Checks that the given <paramref name="value"/> is less than <paramref name="other"/>. 
    /// This method relies on the <see cref="IComparable{T}.CompareTo"/> contract.
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    /// <param name="value">Value to check, can not be <see langword="null"/></param>
    /// <param name="other">Comparison target, can not be <see langword="null"/></param>
    /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
    /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not less than <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
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
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not less than or equal to <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not less than or equal to <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
            [ValidatedNotNull] string customMessage)
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not greater than <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not greater than <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
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
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not greater than or equal to <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
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
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not greater than or equal to <paramref name="other"/>
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
        => OutOfRangeChecks.GreaterThanOrEqualTo(value, other, paramName, customMessage);

    /// <summary>
    /// <p>Checks that the given <paramref name="value"/> is between [<paramref name="fromInclusive"/>, 
    /// <paramref name="toInclusive"/>] (closed range).</p>
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not between [<paramref name="fromInclusive"/>, 
    /// <paramref name="toInclusive"/>]
    /// </exception>
    [DebuggerStepThrough]
    public static TComparable Between<TComparable>(
        [ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? fromInclusive,
        [ValidatedNotNull] TComparable? toInclusive,
        [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.Between(value, fromInclusive, toInclusive, paramName, customMessage);

    #endregion

    #region Checksum algorithms

    private static readonly Regex LuhnDigitsRegex = new("[0-9]{2}", RegexOptions.Compiled);

    /// <summary>
    /// Validates that the given argument (<paramref name="value" />) has a valid checksum digit as described by the 
    /// Luhn algorithm or Luhn formula.
    /// </summary>
    /// <remarks>
    /// See https://en.wikipedia.org/wiki/Luhn_algorithm and https://www.investopedia.com/terms/l/luhn-algorithm.asp
    /// </remarks>
    /// <param name="value">Value to check, can not be <see langword="null"/></param>
    /// <param name="paramName">Parameter name, can not be <see langword="null"/></param>
    /// <param name="customMessage">Custom error message, can not be <see langword="null"/></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
    /// <exception cref="FormatException">
    /// If <paramref name="value"/> is not valid as described by the Luhn algorithm.
    /// </exception>
    [DebuggerStepThrough]
    public static string ValidLuhnChecksum([ValidatedNotNull] string? value,
        [ValidatedNotNull] string paramName, [ValidatedNotNull] string customMessage)
    {
        (string validParamName, string validCustomMessage) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
             customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));

        string notNullValue = NullAndEmptyChecks.NotNull(value, validParamName);

        ValidateLuhnSequenceFormat(notNullValue, validCustomMessage);

        return LuhnFormula.IsValid(ToDigitsArray(notNullValue)) ? notNullValue
            : throw new FormatException(validCustomMessage);
    }

    private static void ValidateLuhnSequenceFormat(string notNullValue, string validCustomMessage)
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
    public static string ValidBase64([ValidatedNotNull] string? value, [ValidatedNotNull] string paramName)
    {
        string validParamName =
            paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName));

        string notNullValue = value.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(validParamName);

        return IsBase64String(notNullValue) ? notNullValue
            : throw new FormatException($"{validParamName} is not a valid Base64 String.");
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
    public static string ValidBase64([ValidatedNotNull] string? value, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage)
    {
        (string validParamName, string validCustomMessage) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
             customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(customMessage)));

        string notNullValue
            = value.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(validParamName, validCustomMessage);

        return IsBase64String(notNullValue) ? notNullValue : throw new FormatException(validCustomMessage);
    }

    private static bool IsBase64String(string base64)
    {
        Span<byte> buffer = new(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out _);
    }

    #endregion // Known Encodings



    #region General Purpose Checks

    /// <summary>
    /// Succeeds if <paramref name="precondition"/> (value or expression) is <see langword="true"/>.
    /// </summary>
    /// <param name="precondition">Boolean expression that must be <see langword="true"/> for the argument check to 
    /// succeed.</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <param name="preconditionDescription">Description for the custom precondition.</param>
    [Obsolete("Please stop using this method, it will be removed on mayor release 4.x. Use CompliesWith(T?, Func<T, bool>, string, string), or DoesNotComplyWith(T?, Func<T, bool>, string, string) instead.", error: false)]
    [DebuggerStepThrough]
    public static void CompliesWith(bool precondition, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string preconditionDescription)
    {
        (string validParamName, string validPreconditionDescription) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName)),
             preconditionDescription.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(preconditionDescription)));

        if (!precondition)
        {
            throw new ArgumentException(paramName: validParamName, message: validPreconditionDescription);
        }
    }

    /// <summary>
    /// Checks the given value for <see langword="null"/> and then if the its complies with the validator function. 
    /// </summary>
    /// <param name="value">To be validated.</param>
    /// <param name="validator">Validator function, expecting to return <see langword="true"/> for the argument 
    /// check to succeed.</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <param name="preconditionDescription">Description for the custom precondition.</param>
    /// <typeparam name="TNullable"></typeparam>
    [DebuggerStepThrough]
    public static TNullable CompliesWith<TNullable>(
        [ValidatedNotNull] TNullable? value,
        [ValidatedNotNull] Func<TNullable, bool> validator,
        [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string preconditionDescription)
            where TNullable : class
            => CompliesWithExpected(value, validator, paramName, preconditionDescription, true);

    /// <summary>
    /// Checks the given value for <see langword="null"/> and then if the its complies with the validator function. 
    /// </summary>
    /// <param name="value">To be validated.</param>
    /// <param name="validator">Validator function, expecting to return <see langword="true"/> for the argument 
    /// check to succeed.</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <param name="preconditionDescription">Description for the custom precondition.</param>
    /// <typeparam name="TNullable"></typeparam>
    [DebuggerStepThrough]
    public static TNullable DoesNotComplyWith<TNullable>(
        [ValidatedNotNull] TNullable? value,
        [ValidatedNotNull] Func<TNullable, bool> validator,
        [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string preconditionDescription)
            where TNullable : class
            => CompliesWithExpected(value, validator, paramName, preconditionDescription, false);

    private static TNullable CompliesWithExpected<TNullable>(
        [ValidatedNotNull] TNullable? value,
        [ValidatedNotNull] Func<TNullable, bool> validator,
        [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string preconditionDescription,
        bool expected)
            where TNullable : class
    {
        TNullable notNullValue = value.ValueOrThrowIfNull(nameof(value));
        Func<TNullable, bool> notNullValidator = validator.ValueOrThrowIfNull(nameof(validator));
        string notNullParamName = paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(paramName));
        string notNullPreconditionDescription
            = preconditionDescription.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(nameof(preconditionDescription));

        if (notNullValidator(notNullValue) != expected)
        {
            throw new ArgumentException(paramName: notNullParamName, message: notNullPreconditionDescription);
        }

        return notNullValue;
    }

    #endregion //General Purpose Checks
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
