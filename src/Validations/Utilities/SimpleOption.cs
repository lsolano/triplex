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
    public static SimpleOption<T> SomeNotNull<T>(in T value)
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
/// Utility option struct for optional pattern.
/// </summary>
public readonly struct SimpleOption<T> : IEquatable<SimpleOption<T>>
{
    private const int DefaultHashcode = 31;

    private readonly T _value;

    internal SimpleOption(in T value, in bool hasValue)
    {
        _value = value;
        HasValue = hasValue;
    }

    /// <summary>
    /// Indicates if this option is not empty.
    /// </summary>
    /// <value></value>
    public bool HasValue { get; }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
    /// <summary>
    /// Wrapped value or exception if the option is empty.
    /// </summary>
    /// <returns></returns>
    public T ValueOrFailure => HasValue ? _value : throw new InvalidOperationException("This option is empty.");

#pragma warning restore CA1303 // Do not pass literals as localized parameters

    /// <summary>
    /// Same as <see cref="Equals(SimpleOption{T})"/> after casting <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is SimpleOption<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    /// <summary>
    /// Indicates if the two options are equals. 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(SimpleOption<T> other)
    {
        if (HasValue)
        {
            return other.HasValue && ValueOrFailure!.Equals(other.ValueOrFailure);
        }

        return !other.HasValue;
    }

    /// <summary>
    /// Returns a hash comming from wrapped value or a default value for empty options.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => HasValue ? ValueOrFailure!.GetHashCode() : DefaultHashcode;

    /// <summary>
    /// Indicates if two instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(SimpleOption<T> left, SimpleOption<T> right) => !left.Equals(right);

    /// <summary>
    /// Indicates if two instances are equals.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(SimpleOption<T> left, SimpleOption<T> right) => left.Equals(right);
}
