namespace Triplex.Validations.ArgumentsHelpers;

internal static class OutOfRangeChecks
{
    [return: NotNull]
    internal static TComparable LessThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));
        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable LessThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable LessThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable LessThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable GreaterThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
                SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable GreaterThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable GreaterThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable GreaterThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.ValueOrThrowIfNull(nameof(other))));

        return CheckBoundaries(value, range, paramName, customMessage.ValueOrThrowIfNull(nameof(customMessage)));
    }

    [return: NotNull]
    internal static TComparable Between<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable fromInclusive,
        [NotNull] TComparable toInclusive,
        [NotNull] string paramName) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = new(
            SimpleOption.SomeNotNull(fromInclusive.ValueOrThrowIfNull()),
            SimpleOption.SomeNotNull(toInclusive.ValueOrThrowIfNull()));

        string notNullParamName = paramName.ValueOrThrowIfNull();

        return range.Contains(
                    value.ValueOrThrowIfNull(notNullParamName),
                    notNullParamName,
                    customMessage: null!);

    }

    [return: NotNull]
    internal static TComparable Between<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable fromInclusive,
        [NotNull] TComparable toInclusive,
        [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = new(
            SimpleOption.SomeNotNull(fromInclusive.ValueOrThrowIfNull(nameof(fromInclusive))),
            SimpleOption.SomeNotNull(toInclusive.ValueOrThrowIfNull(nameof(toInclusive))));

        return range.Contains(
                    value.ValueOrThrowIfNull(nameof(value)),
                    paramName.ValueOrThrowIfNull(nameof(customMessage)),
                    customMessage.ValueOrThrowIfNull(nameof(customMessage)));

    }

    [return: NotNull]
    private static TComparable CheckBoundaries<TComparable>(
        [NotNull] TComparable? value,
        ComparableRange<TComparable> range,
        [NotNull] string paramName,
        string? customMessage) where TComparable : IComparable<TComparable>
            => range.Contains(
                value.ValueOrThrowIfNull(nameof(value)),
                paramName.ValueOrThrowIfNull(nameof(paramName)),
                customMessage);
}
