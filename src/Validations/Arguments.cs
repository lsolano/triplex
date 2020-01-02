using System;

namespace Triplex.Validations
{
    /// <summary>
    /// Utility class used to validate arguments. Useful to check constructor and public methods arguments.
    /// If checks are violated an instance of <see cref="System.ArgumentException" /> is thrown.
    /// </summary>
    public static class Arguments
    {
        #region Null Checks
        /// <summary>
        /// Checks that the provided value is not <see langword="null" />.
        /// </summary>
        /// <typeparam name="TParamType">Type of the value being checked.</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Parameter name, from caller's context.</param>
        /// <returns><paramref name="value"/></returns>
        /// <exception type="System.ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        public static TParamType NotNull<TParamType>(TParamType value, string paramName) where TParamType : class
            => value ?? ThrowArgumentNullException<TParamType>(paramName, null);

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
        /// <exception type="System.ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        public static TParamType NotNull<TParamType>(TParamType value, string paramName, string customMessage) where TParamType : class
            => value ?? ThrowArgumentNullException<TParamType>(paramName, customMessage);

        private static TParamType ThrowArgumentNullException<TParamType>(string paramName, string? customMessage) where TParamType : class
        {
            if (customMessage == null)
            {
                throw new ArgumentNullException(paramName);
            }

            throw new ArgumentNullException(paramName, customMessage);
        }

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
        public static TEnumType ValidEnumerationMember<TEnumType>(TEnumType value, string paramName, string customMessage) where TEnumType : Enum
            => Enum.IsDefined(typeof(TEnumType), value) ? value : ThrowArgumentOutOfRangeExceptionForEnum(value, paramName, customMessage);

        private static TEnumType ThrowArgumentOutOfRangeExceptionForEnum<TEnumType>(TEnumType value, string paramName, string? customMessage) where TEnumType : Enum
        {
            const string valueNotWithinEnumMessageTemplate = "Value is not a member of enum {enumType}.";

            string finalMessage = customMessage
                                  ?? valueNotWithinEnumMessageTemplate.Replace("{enumType}", typeof(TEnumType).Name
                                      #if NETSTANDARD
                                      ,StringComparison.Ordinal
                                      #endif
                                      );

            throw new ArgumentOutOfRangeException(paramName, value, finalMessage);
        }

        #endregion

        #region Out-of-range Checks

        private sealed class ComparableRange<TComparable> where TComparable : struct, IComparable<TComparable>
        {
            internal ComparableRange(TComparable? min, TComparable? max) :this (min, true, max, true)
            {
            }

            internal ComparableRange(TComparable? min, bool minInclusive, TComparable? max, bool maxInclusive)
            {
                if (!min.HasValue && !max.HasValue)
                {
                    throw new ArgumentException("Useless range detected, no min or max boundaries.");
                }

                if (min.HasValue && max.HasValue)
                {
                    bool minIsEqualsToOrGreaterThanMax = min.Value.CompareTo(max.Value) >= 0;
                    if (minIsEqualsToOrGreaterThanMax)
                    {
                        throw new ArgumentOutOfRangeException(nameof(min), min, "Must be less than {max}.");
                    }
                }

                Min = min;
                Max = max;
                MinInclusive = minInclusive;
                MaxInclusive = maxInclusive;
            }

            private TComparable? Min { get; }
            private TComparable? Max { get; }
            private bool MinInclusive { get; }
            private bool MaxInclusive { get; }

            internal TComparable IsWithin(TComparable value, string paramName, string customMessage)
            {
                CheckLowerBoundary(value, paramName, customMessage);

                CheckUpperBoundary(value, paramName, customMessage);

                return value;
            }

            private void CheckLowerBoundary(TComparable value, string paramName, string customMessage)
            {
                if (!Min.HasValue)
                {
                    return;
                }

                int valueVersusMinimum = value.CompareTo(Min.Value);
                bool valueBelowMinimum = MinInclusive ? valueVersusMinimum < 0 : valueVersusMinimum <= 0;

                if (!valueBelowMinimum)
                {
                    return;
                }

                string orEqualToOption = MinInclusive ? "or equal to " : string.Empty;
                throw new ArgumentOutOfRangeException(paramName, value, customMessage ?? $"Must be greater than {orEqualToOption}{Min}.");
            }

            private void CheckUpperBoundary(TComparable value, string paramName, string customMessage)
            {
                if (!Max.HasValue)
                {
                    return;
                }

                int valueVersusMaximum = value.CompareTo(Max.Value);
                bool valueIsAboveMaximum = MaxInclusive ? valueVersusMaximum > 0 : valueVersusMaximum >= 0;

                if (!valueIsAboveMaximum)
                {
                    return;
                }

                string orEqualToOption = MaxInclusive ? "or equal to " : string.Empty;
                throw new ArgumentOutOfRangeException(paramName, value, customMessage ?? $"Must be less than {orEqualToOption}{Max}.");
            }
        }

        public static TComparable LessThan<TComparable>(TComparable value, TComparable other, string paramName,
            string customMessage) where TComparable : struct, IComparable<TComparable>
        {
            return CheckBoundaries(value, null, other, paramName, customMessage);
        }

        public static TComparable GreaterThan<TComparable>(TComparable value, TComparable other, string paramName,
            string customMessage) where TComparable : struct, IComparable<TComparable>
        {
            return CheckBoundaries(value, other, null, paramName, customMessage);
        }

        private static TComparable CheckBoundaries<TComparable>(TComparable value, TComparable? min, TComparable? max,
            string paramName, string customMessage) where TComparable : struct, IComparable<TComparable>
        {
            var range = new ComparableRange<TComparable>(min, max);

            return range.IsWithin(value, paramName, customMessage);
        }

        #endregion
    }
}
