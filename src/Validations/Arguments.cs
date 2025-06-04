using System.Runtime.CompilerServices;
using Triplex.Validations.Algorithms.Checksum;

namespace Triplex.Validations;

/// <summary>
/// Utility class used to validate arguments. Useful to check constructor and public methods arguments.
/// If checks are violated an instance of <see cref="ArgumentException" /> is thrown.
/// All checks imply an initial Not-Null check for all values checked, 
/// so <code>Arguments.OrException(someParam);</code> means "Give 'someParam' value back or throw exception if it is <see langword="null"/> ."
/// </summary>
public static partial class Arguments
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
    [return: NotNull]
    public static TParamType OrException<TParamType>(
        [NotNull] TParamType? value,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TParamType : class
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
    [return: NotNull]
    public static TParamType OrExceptionWithMessage<TParamType>(
        [NotNull] TParamType? value,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
            where TParamType : class
        => NullAndEmptyChecks.NotNull(value, paramName, customMessage);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains white-space 
    /// only characters.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any parameter is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    /// <exception cref="ArgumentFormatException">
    /// If <paramref name="value"/> contains only white-space characters
    /// </exception>
    [DebuggerStepThrough]
    [return: NotNull]
    public static string NotEmptyNorWhiteSpaceOnlyOrException(
        [NotNull] string? value,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
        => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" />, empty (zero length), or contains white-space 
    /// only characters.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <param name="customMessage">Custom exception error message</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any parameter is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    /// <exception cref="ArgumentFormatException">
    /// If <paramref name="value"/> contains only white-space characters
    /// </exception>
    [DebuggerStepThrough]
    [return: NotNull]
    public static string NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(
        [NotNull] string? value,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
        => NullAndEmptyChecks.NotNullEmptyOrWhiteSpaceOnly(value, paramName, customMessage);

    /// <summary>
    /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any parameter is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    /// <exception cref="ArgumentFormatException">
    /// If <paramref name="value"/> contains only white-space characters
    /// </exception>
    [DebuggerStepThrough]
    [return: NotNull]
    public static string NotEmptyOrException(
        [NotNull] string? value,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
        => NullAndEmptyChecks.NotNullOrEmpty(value, paramName);

    /// <summary>
    /// Checks that the provided value is not empty.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter name, from caller's context.</param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is an empty <see cref="Guid"/>.</exception>
    [DebuggerStepThrough]
    public static Guid NotEmptyOrException(Guid value,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        string validParamName = paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly();

        return IsEmpty(value) ? throw new ArgumentException(validParamName) : value;
    }

    /// <summary>
    /// Checks that the provided value is not <see langword="null" /> or empty (zero length).
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Parameter's name, can not be <see langword="null" /></param>
    /// <param name="customMessage">Custom message, can not be <see langword="null" /></param>
    /// <returns><paramref name="value"/></returns>
    /// <exception cref="ArgumentNullException">If any parameter is <see langword = "null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> length is zero.</exception>
    [DebuggerStepThrough]
    [return: NotNull]
    public static string NotEmptyOrExceptionWithMessage(
        [NotNull] string? value,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
        => NullAndEmptyChecks.NotNullOrEmpty(value, paramName, customMessage);

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
    public static Guid NotEmptyOrExceptionWithMessage(Guid value,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        (string validParamName, string validCustomMessage) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(),
             customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly());

        return IsEmpty(value) ?
            throw new ArgumentException(paramName: validParamName, message: validCustomMessage)
            : value;
    }

    private static bool IsEmpty(Guid value) => value == default;

    #endregion

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
    [return: NotNull]
    public static TEnumType MemberOfOrException<TEnumType>(TEnumType value,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
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
    [return: NotNull]
    public static TEnumType MemberOfOrExceptionWithMessage<TEnumType>(TEnumType value,
        string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TEnumType : Enum
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
    [return: NotNull]
    public static TComparable LessThanOrException<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    [return: NotNull]
    public static TComparable LessThanOrExceptionWithMessage<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    [return: NotNull]
    public static TComparable LessThanOrEqualToOrException<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    [return: NotNull]
    public static TComparable LessThanOrEqualToOrExceptionWithMessage<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    [return: NotNull]
    public static TComparable GreaterThanOrException<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    [return: NotNull]
    public static TComparable GreaterThanOrExceptionWithMessage<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    [return: NotNull]
    public static TComparable GreaterThanOrEqualToOrException<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
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
    [return: NotNull]
    public static TComparable GreaterThanOrEqualToOrExceptionWithMessage<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable other,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
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
    /// <typeparam name="TComparable"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="value"/> is not between [<paramref name="fromInclusive"/>, 
    /// <paramref name="toInclusive"/>]
    /// </exception>
    [DebuggerStepThrough]
    [return: NotNull]
    public static TComparable BetweenOrException<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable fromInclusive,
        [NotNull] TComparable toInclusive,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.Between(value, fromInclusive, toInclusive, paramName);

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
    [return: NotNull]
    public static TComparable BetweenOrExceptionWithMessage<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable fromInclusive,
        [NotNull] TComparable toInclusive,
        [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "") where TComparable : IComparable<TComparable>
            => OutOfRangeChecks.Between(value, fromInclusive, toInclusive, paramName, customMessage);

    #endregion

    #region Checksum algorithms

    private static readonly Regex LuhnDigitsRegex = TwoOrMoreDigitsRegex();

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
    [return: NotNull]
    public static string ValidLuhnChecksum([NotNull] string? value, [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        (string validParamName, string validCustomMessage) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(),
             customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly());

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
        return [.. notNullValue.Select(ch => ch - zeroAsciiCode)];
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
    public static string ValidBase64([NotNull] string? value, [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        string validParamName =
            paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly();

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
    public static string ValidBase64([NotNull] string? value, [NotNull] string customMessage,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        (string validParamName, string validCustomMessage) =
            (paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly(),
             customMessage.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly());

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
        [NotNull] TNullable? value,
        [NotNull] Func<TNullable, bool> validator,
        [NotNull] string preconditionDescription,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
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
        [NotNull] TNullable? value,
        [NotNull] Func<TNullable, bool> validator,
        [NotNull] string preconditionDescription,
        [NotNull, CallerArgumentExpression(nameof(value))] string paramName = "")
            where TNullable : class
            => CompliesWithExpected(value, validator, paramName, preconditionDescription, false);

    [return: NotNull]
    private static TNullable CompliesWithExpected<TNullable>(
        [NotNull] TNullable? value,
        [NotNull] Func<TNullable, bool> validator,
        [NotNull] string preconditionDescription,
        [NotNull] string paramName,
        bool expected) where TNullable : class
    {
        TNullable notNullValue = value.ValueOrThrowIfNull(nameof(value));
        Func<TNullable, bool> notNullValidator = validator.ValueOrThrowIfNull();
        string notNullParamName = paramName.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly();
        string notNullPreconditionDescription
            = preconditionDescription.ValueOrThrowIfNullZeroLengthOrWhiteSpaceOnly();

        return notNullValidator(notNullValue) != expected
            ? throw new ArgumentException(paramName: notNullParamName, message: notNullPreconditionDescription)
            : notNullValue;
    }

    [GeneratedRegex("[0-9]{2}"), ExcludeFromCodeCoverage]
    private static partial Regex TwoOrMoreDigitsRegex();

    #endregion //General Purpose Checks
}
