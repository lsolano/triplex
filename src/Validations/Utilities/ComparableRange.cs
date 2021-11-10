#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Triplex.Validations.Utilities
{
    /// <summary>
    /// Comparable ranges factory. 
    /// </summary>
    public static class ComparableRangeFactory
    {
        internal static ComparableRange<T> WithMinInclusiveOnly<T>(in SimpleOption<T> min) where T : IComparable<T>
            => new(min, true, SimpleOption.None<T>(), false);

        internal static ComparableRange<T> WithMinExclusiveOnly<T>(in SimpleOption<T> min) where T : IComparable<T>
            => new(min, false, SimpleOption.None<T>(), false);

        internal static ComparableRange<T> WithMaxInclusiveOnly<T>(in SimpleOption<T> max) where T : IComparable<T>
            => new(SimpleOption.None<T>(), false, max, true);

        internal static ComparableRange<T> WithMaxExclusiveOnly<T>(in SimpleOption<T> max) where T : IComparable<T>
            => new(SimpleOption.None<T>(), false, max, false);

        /// <summary>
        /// Builds a range including both ends. 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ComparableRange<T> WithBothInclusive<T>(T min, T max) where T : IComparable<T>
            => new(SimpleOption.SomeNotNull(min), true, SimpleOption.SomeNotNull(max), true);
    }

    /// <summary>
    /// Helper class for abstract ranges.
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    public sealed class ComparableRange<TComparable> where TComparable : IComparable<TComparable>
    {
        internal ComparableRange(in SimpleOption<TComparable> min, in SimpleOption<TComparable> max)
            : this(min, true, max, true)
        {
        }

        internal ComparableRange(in SimpleOption<TComparable> min, in bool minInclusive,
            in SimpleOption<TComparable> max, in bool maxInclusive)
        {
            ValidateRangeArguments(min, minInclusive, max, maxInclusive);

            Min = min;
            Max = max;
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
        }

        private static void ValidateRangeArguments(SimpleOption<TComparable> min, bool minInclusive,
            SimpleOption<TComparable> max, bool maxInclusive)
        {
            ThrowIfBothEmpty(min, max);
            ThrowIfInvertedOrClosed(min, max);
            ThrowIfRequiredEndIsMissing(min, minInclusive, max, maxInclusive);
        }

        private static void ThrowIfBothEmpty(SimpleOption<TComparable> min, SimpleOption<TComparable> max)
        {
            if (!min.HasValue && !max.HasValue)
            {
                throw new ArgumentException("Useless range detected, no min or max boundaries.");
            }
        }

        private static void ThrowIfInvertedOrClosed(SimpleOption<TComparable> min, SimpleOption<TComparable> max)
        {
            if (min.HasValue && max.HasValue)
            {
                bool minIsEqualsToOrGreaterThanMax = min.ValueOrFailure.CompareTo(max.ValueOrFailure) >= 0;
                if (minIsEqualsToOrGreaterThanMax)
                {
                    throw new ArgumentOutOfRangeException(nameof(min), min, $"Must be less than {max.ValueOrFailure}.");
                }
            }
        }

        private static void ThrowIfRequiredEndIsMissing(SimpleOption<TComparable> min, bool minInclusive,
            SimpleOption<TComparable> max, bool maxInclusive)
        {
            if (!min.HasValue && minInclusive)
            {
                throw new ArgumentException(message: "Must have value.", paramName: nameof(min));
            }

            if (!max.HasValue && maxInclusive)
            {
                throw new ArgumentException(message: "Must have value.", paramName: nameof(max));
            }
        }

        private SimpleOption<TComparable> Min { get; }
        private SimpleOption<TComparable> Max { get; }
        private bool MinInclusive { get; }
        private bool MaxInclusive { get; }

        internal TComparable IsWithin(in TComparable value, in string paramName, in string? customMessage)
        {
            CheckLowerBoundary(value, paramName, customMessage);

            CheckUpperBoundary(value, paramName, customMessage);

            return value;
        }

        private void CheckLowerBoundary(in TComparable value, in string paramName, in string? customMessage)
        {
            if (!Min.HasValue)
            {
                return;
            }

            int valueVersusMinimum = value.CompareTo(Min.ValueOrFailure);
            bool valueBelowMinimum = MinInclusive ? valueVersusMinimum < 0 : valueVersusMinimum <= 0;

            if (!valueBelowMinimum)
            {
                return;
            }

            string orEqualToOption = MinInclusive ? "or equal to " : string.Empty;
            throw new ArgumentOutOfRangeException(paramName, value,
                customMessage ?? $"Must be greater than {orEqualToOption}{Min.ValueOrFailure}.");
        }

        private void CheckUpperBoundary(in TComparable value, in string paramName, in string? customMessage)
        {
            if (!Max.HasValue)
            {
                return;
            }

            int valueVersusMaximum = value.CompareTo(Max.ValueOrFailure);
            bool valueIsAboveMaximum = MaxInclusive ? valueVersusMaximum > 0 : valueVersusMaximum >= 0;

            if (!valueIsAboveMaximum)
            {
                return;
            }

            string orEqualToOption = MaxInclusive ? "or equal to " : string.Empty;
            throw new ArgumentOutOfRangeException(paramName, value,
                customMessage ?? $"Must be less than {orEqualToOption}{Max.ValueOrFailure}.");
        }
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
