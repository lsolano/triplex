using System;
using Triplex.ProtoDomainPrimitives.Exceptions;

namespace Triplex.ProtoDomainPrimitives.Numerics
{
    /// <summary>
    /// Valid positive integer, meaning <code>>= 1</code> (greater than or equals to one).
    /// </summary>
    public sealed class PositiveInteger : AbstractDomainPrimitive<int>
    {
        /// <summary>
        /// Error message used when not provided.
        /// </summary>
        public static readonly Message DefaultErrorMessage = new Message("'rawValue' must be positive.");

        /// <summary>
        /// Wraps the raw value and returns a new instance.
        /// </summary>
        /// <param name="rawValue">Must be positive</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="rawValue"/> is zero or negative.</exception>
        public PositiveInteger(int rawValue) : this(rawValue, DefaultErrorMessage)
        {
        }

        /// <summary>
        /// Wraps the raw value and returns a new instance.
        /// </summary>
        /// <param name="rawValue">Must be positive</param>
        /// <param name="errorMessage">Custom error message</param>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="rawValue"/> is zero or negative.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="errorMessage"/> is <see langword="null"/>.</exception>
        public PositiveInteger(int rawValue, Message errorMessage) : base(rawValue, errorMessage, Validate)
        {
        }

        private static int Validate(int rawValue, Message errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            if (rawValue < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(rawValue), rawValue, errorMessage.Value);
            }

            return rawValue;
        }
    }
}
