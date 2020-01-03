using System;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Triplex.Validations.Utilities
{
    internal static class ComparableRangeFactory
    {
        internal static ComparableRange<T> WithMinInclusiveOnly<T>(SimpleOption<T> min) where T : IComparable<T>
            => new ComparableRange<T>(min, true, SimpleOption.None<T>(), false);

        internal static ComparableRange<T> WithMinExclusiveOnly<T>(SimpleOption<T> min) where T : IComparable<T>
            => new ComparableRange<T>(min, false, SimpleOption.None<T>(), false);

        internal static ComparableRange<T> WithMaxInclusiveOnly<T>(SimpleOption<T> max) where T : IComparable<T>
            => new ComparableRange<T>(SimpleOption.None<T>(), false, max, true);

        internal static ComparableRange<T> WithMaxExclusiveOnly<T>(SimpleOption<T> max) where T : IComparable<T>
            => new ComparableRange<T>(SimpleOption.None<T>(), false, max, false);
    }

    internal sealed class ComparableRange<TComparable> where TComparable : IComparable<TComparable>
    {
        internal ComparableRange(SimpleOption<TComparable> min, SimpleOption<TComparable> max) : this(min, true, max,
            true)
        {
        }

        internal ComparableRange(SimpleOption<TComparable> min, bool minInclusive, SimpleOption<TComparable> max,
            bool maxInclusive)
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
                    throw new ArgumentOutOfRangeException(nameof(min), min, $"Must be less than {max.Value}.");
                }
            }

            Min = min;
            Max = max;
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
        }

        private SimpleOption<TComparable> Min { get; }
        private SimpleOption<TComparable> Max { get; }
        private bool MinInclusive { get; }
        private bool MaxInclusive { get; }

        internal TComparable IsWithin(TComparable value, string paramName, string? customMessage)
        {
            CheckLowerBoundary(value, paramName, customMessage);

            CheckUpperBoundary(value, paramName, customMessage);

            return value;
        }

        private void CheckLowerBoundary(TComparable value, string paramName, string? customMessage)
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
            throw new ArgumentOutOfRangeException(paramName, value,
                customMessage ?? $"Must be greater than {orEqualToOption}{Min.Value}.");
        }

        private void CheckUpperBoundary(TComparable value, string paramName, string? customMessage)
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
            throw new ArgumentOutOfRangeException(paramName, value,
                customMessage ?? $"Must be less than {orEqualToOption}{Max.Value}.");
        }
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters