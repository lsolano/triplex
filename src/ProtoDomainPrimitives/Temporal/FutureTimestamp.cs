﻿using System;
using Triplex.ProtoDomainPrimitives.Exceptions;

namespace Triplex.ProtoDomainPrimitives.Temporal
{
    /// <summary>
    /// Represents a <see cref="DateTimeOffset"/> in the future, respect to the system time.
    /// </summary>
    public sealed class FutureTimestamp : AbstractDomainPrimitive<DateTimeOffset>
    {
        /// <summary>
        /// Default error message.
        /// </summary>
        public static readonly Message DefaultErrorMessage = new Message("'rawValue' must be in the future respect to the system time.");

        /// <summary>
        /// Validates input and builds new instance if everything is OK.
        /// </summary>
        /// <param name="rawValue"></param>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="rawValue"/> is not in the future.</exception>
        public FutureTimestamp(DateTimeOffset rawValue) : this(rawValue, DefaultErrorMessage)
        {
        }

        /// <summary>
        /// Validates input and builds new instance if everything is OK.
        /// </summary>
        /// <param name="rawValue">Must be in the future</param>
        /// <param name="errorMessage">Custom error message</param>
        /// <exception cref="ArgumentNullException">When <paramref name="errorMessage"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="rawValue"/> is not in the future.</exception>
        public FutureTimestamp(DateTimeOffset rawValue, Message errorMessage) : base(rawValue, errorMessage, Validate)
        {
        }

        private static DateTimeOffset Validate(DateTimeOffset rawValue, Message errorMessage) {
            if (errorMessage == null) {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            if (rawValue.ToUniversalTime() <= DateTimeOffset.UtcNow) {
                throw new ArgumentOutOfRangeException(nameof(rawValue), rawValue, errorMessage.Value);
            }

            return rawValue;
        }
    }
}