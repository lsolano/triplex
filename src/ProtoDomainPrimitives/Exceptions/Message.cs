using System;
using Triplex.ProtoDomainPrimitives.Strings;

namespace Triplex.ProtoDomainPrimitives.Exceptions
{
    /// <summary>
    /// Non-empty or null exception message.
    /// </summary>
    public sealed class Message : IDomainPrimitive<string>
    {
        private readonly NonEmptyOrWhiteSpaceString _wrapped;

        /// <summary>
        /// Validates input and returns new instance if everything is OK.
        /// </summary>
        /// <param name="rawValue">Can not be <see langword="null"/> or empty</param>
        /// <exception cref="ArgumentNullException">When <paramref name="rawValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">When <paramref name="rawValue"/> is empty or contains only white-spaces.</exception>
        public Message(string rawValue) => _wrapped = new NonEmptyOrWhiteSpaceString(rawValue);

        /// <summary>
        /// Wrapped value.
        /// </summary>
        public string Value => _wrapped.Value;

        /// <summary>
        /// Same as wrapped value comparison.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IDomainPrimitive<string> other) => _wrapped.CompareTo(other);

        /// <summary>
        /// Same as wrapped value equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDomainPrimitive<string> other) => _wrapped.Equals(other);
    }
}
