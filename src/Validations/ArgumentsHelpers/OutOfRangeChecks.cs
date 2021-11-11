using Triplex.Validations.Utilities;

namespace Triplex.Validations.ArgumentsHelpers;

internal static class OutOfRangeChecks
{
    internal static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));
        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable LessThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable GreaterThan<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? other, [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable Between<TComparable>(
        [ValidatedNotNull] TComparable? value,
        [ValidatedNotNull] TComparable? fromInclusive,
        [ValidatedNotNull] TComparable? toInclusive,
        [ValidatedNotNull] string paramName,
        [ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = new(
            SimpleOption.SomeNotNull(fromInclusive.ValueOrThrowIfNull(nameof(fromInclusive))),
            SimpleOption.SomeNotNull(toInclusive.ValueOrThrowIfNull(nameof(toInclusive))));

        return range.IsWithin(
                    value.ValueOrThrowIfNull(nameof(value)),
                    paramName.ValueOrThrowIfNull(nameof(customMessage)),
                    customMessage.ValueOrThrowIfNull(nameof(customMessage)));

    }

    private static TComparable CheckBoundaries<TComparable>(
        [ValidatedNotNull] TComparable? value,
        ComparableRange<TComparable> range,
        [ValidatedNotNull] string paramName,
        string? customMessage) where TComparable : IComparable<TComparable>
    {
        return range.IsWithin(value.ValueOrThrowIfNull(nameof(value)), paramName.ValueOrThrowIfNull(nameof(paramName)),
            customMessage);
    }
}
