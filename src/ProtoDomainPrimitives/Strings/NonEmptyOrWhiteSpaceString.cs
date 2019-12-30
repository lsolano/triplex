using System;

namespace Triplex.ProtoDomainPrimitives.Strings
{
    /// <summary>
    /// Non empty or white-space only string.
    /// </summary>
    public sealed class NonEmptyOrWhiteSpaceString : IDomainPrimitive<string>
    {
        /// <summary>
        /// Comparison strategy used.
        /// </summary>
        public const StringComparison ComparisonStrategy = StringComparison.Ordinal;

        /// <summary>
        /// Default error message.
        /// </summary>
        public const string DefaultErrorMessage = "'rawValue' can not be empty or white space only.";

        /// <summary>
        /// Error message used when <code>errorMessage</code> parameter is invalid.
        /// </summary>
        public const string InvalidCustomErrorMessageMessage = "'errorMessage' could not be null, empty or white-space only.";

        /// <summary>
        /// Validates input and returns new instance if everything is OK.
        /// </summary>
        /// <param name="rawValue">Can not be <see langword="null"/> or empty</param>
        /// <exception cref="ArgumentNullException">When <paramref name="rawValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">When <paramref name="rawValue"/> is empty or contains only white-spaces.</exception>
        public NonEmptyOrWhiteSpaceString(string rawValue) => Value = Validate(rawValue, DefaultErrorMessage);

        /// <summary>
        /// Validates input and returns new instance if everything is OK.
        /// </summary>
        /// <param name="rawValue">Can not be <see langword="null"/> or empty</param>
        /// <param name="errorMessage">Custom error message</param>
        /// <exception cref="ArgumentNullException">When any parameter is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">When <paramref name="rawValue"/> is empty or contains only white-spaces.</exception>
        public NonEmptyOrWhiteSpaceString(string rawValue, string errorMessage) => Value = Validate(rawValue, errorMessage);

        private static string Validate(string rawValue, string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException(nameof(errorMessage), InvalidCustomErrorMessageMessage);
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw  new FormatException(InvalidCustomErrorMessageMessage);
            }

            if (rawValue == null)
            {
                throw new ArgumentNullException(nameof(rawValue), errorMessage);
            }

            if (string.IsNullOrWhiteSpace(rawValue))
            {
                throw new FormatException(errorMessage);
            }

            return rawValue;
        }

        ///<inheritdoc cref="IDomainPrimitive{TRawType}.Value"/>
        public string Value { get; }

        /// <summary>
        /// Same as wrapped value.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Same as wrapped value.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Value;

        /// <summary>
        /// Same as wrapped value.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj) => Equals(obj as NonEmptyOrWhiteSpaceString);

        /// <summary>
        /// Same as wrapped value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IDomainPrimitive<string> other)
            => string.Compare(Value, other?.Value, ComparisonStrategy);

        /// <summary>
        /// Same as wrapped value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDomainPrimitive<string>? other)
        {
            if (other == null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || Value.Equals(other.Value, ComparisonStrategy);
        }
    }
}
