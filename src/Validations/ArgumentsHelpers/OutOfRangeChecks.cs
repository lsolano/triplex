namespace Triplex.Validations.ArgumentsHelpers;

internal static class OutOfRangeChecks
{
    [return: NotNull]
    internal static TComparable LessThan<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));
        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable LessThan<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName,
        [NotNull, ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable LessThanOrEqualTo<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable LessThanOrEqualTo<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName,
        [NotNull, ValidatedNotNull] string customMessage)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable GreaterThan<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable GreaterThan<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName,
        [NotNull, ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable GreaterThanOrEqualTo<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable GreaterThanOrEqualTo<TComparable>([NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? other, [NotNull, ValidatedNotNull] string paramName,
        [NotNull, ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable Between<TComparable>(
        [NotNull, ValidatedNotNull] TComparable? value,
        [NotNull, ValidatedNotNull] TComparable? fromInclusive,
        [NotNull, ValidatedNotNull] TComparable? toInclusive,
        [NotNull, ValidatedNotNull] string paramName,
        [NotNull, ValidatedNotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = new(
            SimpleOption.SomeNotNull(fromInclusive.ValueOrThrowIfNull(nameof(fromInclusive))),
            SimpleOption.SomeNotNull(toInclusive.ValueOrThrowIfNull(nameof(toInclusive))));

        return range.IsWithin(
                    value.ValueOrThrowIfNull(nameof(value)),
                    paramName.ValueOrThrowIfNull(nameof(customMessage)),
                    customMessage.ValueOrThrowIfNull(nameof(customMessage)));

    }

    [return: NotNull]
    private static TComparable CheckBoundaries<TComparable>(
        [NotNull, ValidatedNotNull] TComparable? value,
        ComparableRange<TComparable> range,
        [NotNull, ValidatedNotNull] string paramName,
        string? customMessage) where TComparable : IComparable<TComparable>
    {
        return range.IsWithin(value.ValueOrThrowIfNull(nameof(value)), paramName.ValueOrThrowIfNull(nameof(paramName)),
            customMessage);
    }
}
