namespace Triplex.Validations.Utilities;

/// <summary>
/// Options builder class.
/// </summary>
public static class SimpleOption
{
    /// <summary>
    /// Returns a non-empty option if the value is not null.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static SimpleOption<T> SomeNotNull<T>(T value)
        => new(value, value is not null);

    /// <summary>
    /// Returns an empty option.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static SimpleOption<T> None<T>() //NOSONAR
        => new(default!, false);
}

/// <summary>
/// Utility option for optional pattern.
/// </summary>
public record SimpleOption<T>(T Value, bool HasValue) : IEquatable<SimpleOption<T>>
{
    private const int DefaultHashcode = 31;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
    /// <summary>
    /// Wrapped value or exception if the option is empty.
    /// </summary>
    /// <returns></returns>
    public T ValueOrFailure => HasValue ? Value : throw new InvalidOperationException("This option is empty.");

#pragma warning restore CA1303 // Do not pass literals as localized parameters

    /// <summary>
    /// Returns a hash comming from wrapped value or a default value for empty options.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => HasValue ? ValueOrFailure!.GetHashCode() : DefaultHashcode;
}
