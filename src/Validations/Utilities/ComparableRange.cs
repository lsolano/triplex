#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Triplex.Validations.Utilities;

/// <summary>
/// Comparable ranges factory. 
/// </summary>
public static class ComparableRangeFactory
{
    internal static ComparableRange<T> WithMinInclusiveOnly<T>(SimpleOption<T> min) where T : IComparable<T>
        => new(min, true, SimpleOption.None<T>(), false);

    internal static ComparableRange<T> WithMinExclusiveOnly<T>(SimpleOption<T> min) where T : IComparable<T>
        => new(min, false, SimpleOption.None<T>(), false);

    internal static ComparableRange<T> WithMaxInclusiveOnly<T>(SimpleOption<T> max) where T : IComparable<T>
        => new(SimpleOption.None<T>(), false, max, true);

    internal static ComparableRange<T> WithMaxExclusiveOnly<T>(SimpleOption<T> max) where T : IComparable<T>
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
    internal ComparableRange(SimpleOption<TComparable> min, SimpleOption<TComparable> max)
        : this(min, true, max, true)
    {
    }

    internal ComparableRange(SimpleOption<TComparable> min, bool minInclusive,
        SimpleOption<TComparable> max, bool maxInclusive)
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

    internal TComparable Contains(TComparable value, string paramName, string? customMessage)
    {
        CheckLowerBoundary(value, paramName, customMessage);

        CheckUpperBoundary(value, paramName, customMessage);

        return value;
    }

    private void CheckLowerBoundary(TComparable value, string paramName, string? customMessage)
    {
        if (!Min.HasValue || ValueIsNotBelowMinimum(value))
        {
            return;
        }

        ThrowBoundaryViolatedException(value, paramName, customMessage, BoundaryViolations.Lower);
    }

    private bool ValueIsNotBelowMinimum(TComparable value)
    {
        int valueVersusMinimum = value.CompareTo(Min.ValueOrFailure);

        return !(MinInclusive ? valueVersusMinimum < 0 : valueVersusMinimum <= 0);
    }

    private void CheckUpperBoundary(TComparable value, string paramName, string? customMessage)
    {
        if (!Max.HasValue || ValueIsNotAboveMaximum(value))
        {
            return;
        }

        ThrowBoundaryViolatedException(value, paramName, customMessage, BoundaryViolations.Upper);
    }

    private bool ValueIsNotAboveMaximum(TComparable value)
    {
        int valueVersusMaximum = value.CompareTo(Max.ValueOrFailure);

        return !(MaxInclusive ? valueVersusMaximum > 0 : valueVersusMaximum >= 0);
    }

    private void ThrowBoundaryViolatedException(TComparable value, string paramName, string? customMessage,
        BoundaryViolations violation)
    {
        static string OrEqualToOptionMessage(bool inclusive) => inclusive ? "or equal to " : string.Empty;

        if (violation == BoundaryViolations.Lower)
        {
            throw new ArgumentOutOfRangeException(paramName, value,
                customMessage ?? $"Must be greater than {OrEqualToOptionMessage(MinInclusive)}{Min.ValueOrFailure}.");
        }

        throw new ArgumentOutOfRangeException(paramName, value,
            customMessage ?? $"Must be less than {OrEqualToOptionMessage(MaxInclusive)}{Max.ValueOrFailure}.");
    }

    private enum BoundaryViolations { Lower = 0, Upper = 1 }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters
