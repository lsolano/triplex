namespace Triplex.Validations.ArgumentsHelpers;

internal static class OutOfRangeChecks
{
    [return: NotNull]
    internal static TComparable LessThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.Check()));
        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable LessThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxExclusiveOnly(
                SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, customMessage.CheckExceptionMessage());
    }

    [return: NotNull]
    internal static TComparable LessThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable LessThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMaxInclusiveOnly(
                SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, customMessage.CheckExceptionMessage());
    }

    [return: NotNull]
    internal static TComparable GreaterThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
                SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable GreaterThan<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinExclusiveOnly(
            SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, customMessage.CheckExceptionMessage());
    }

    [return: NotNull]
    internal static TComparable GreaterThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName)
        where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, null);
    }

    [return: NotNull]
    internal static TComparable GreaterThanOrEqualTo<TComparable>([NotNull] TComparable? value,
        [NotNull] TComparable other, [NotNull] string paramName,
        [NotNull] string customMessage) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = ComparableRangeFactory.WithMinInclusiveOnly(
            SimpleOption.SomeNotNull(other.Check()));

        return CheckBoundaries(value, range, paramName, customMessage.CheckExceptionMessage());
    }

    [return: NotNull]
    internal static TComparable Between<TComparable>(
        [NotNull] TComparable? value,
        [NotNull] TComparable fromInclusive,
        [NotNull] TComparable toInclusive,
        [NotNull] string paramName) where TComparable : IComparable<TComparable>
    {
        ComparableRange<TComparable> range = new(
            SimpleOption.SomeNotNull(fromInclusive.Check()),
            SimpleOption.SomeNotNull(toInclusive.Check()));

        string notNullParamName = paramName.CheckParamName();

        return range.Contains(
                    value.Check(notNullParamName),
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
            SimpleOption.SomeNotNull(fromInclusive.Check()),
            SimpleOption.SomeNotNull(toInclusive.Check()));

        return range.Contains(
                    value.Check(),
                    paramName.CheckParamName(),
                    customMessage.CheckExceptionMessage());

    }

    [return: NotNull]
    private static TComparable CheckBoundaries<TComparable>(
        [NotNull] TComparable? value,
        ComparableRange<TComparable> range,
        [NotNull] string paramName,
        string? customMessage) where TComparable : IComparable<TComparable>
            => range.Contains(
                value.Check(),
                paramName.CheckParamName(),
                customMessage);
}
