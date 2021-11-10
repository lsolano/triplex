using Triplex.Validations.Utilities;

namespace Triplex.Validations.ArgumentsHelpers;

internal static class OutOfRangeChecks
{
    internal static TComparable LessThan<TComparable>([ValidatedNotNull] in TComparable? value,
        [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));
        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable LessThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName,
        [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value,
        [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable LessThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value,
        [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName, [ValidatedNotNull] in string customMessage)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable GreaterThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable GreaterThan<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName,
        [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    internal static TComparable GreaterThanOrEqualTo<TComparable>([ValidatedNotNull] in TComparable? value, [ValidatedNotNull] in TComparable? other, [ValidatedNotNull] in string paramName,
        [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    internal static TComparable Between<TComparable>(
        [ValidatedNotNull] in TComparable? value,
        [ValidatedNotNull] in TComparable? fromInclusive,
        [ValidatedNotNull] in TComparable? toInclusive,
        [ValidatedNotNull] in string paramName,
        [ValidatedNotNull] in string customMessage) where TComparable : IComparable<TComparable>
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
        [ValidatedNotNull] in TComparable? value,
        in ComparableRange<TComparable> range,
        [ValidatedNotNull] in string paramName,
        in string? customMessage) where TComparable : IComparable<TComparable>
    {
        return range.IsWithin(value.ValueOrThrowIfNull(nameof(value)), paramName.ValueOrThrowIfNull(nameof(paramName)), customMessage);
    }
}
