using System;

namespace Triplex.Validations.Utilities
{
    internal static class SimpleOption
    {
        internal static SimpleOption<T> SomeNotNull<T>(in T value)
            => new SimpleOption<T>(value, value is not null);

        internal static SimpleOption<T> None<T>()
            => new SimpleOption<T>(default!, false);
    }

    internal struct SimpleOption<T>
    {
        private readonly T _value;

        internal SimpleOption(in T value, in bool hasValue) {
            _value = value;
            HasValue = hasValue;
        }

        internal bool HasValue { get; }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
        internal T Value => HasValue ? _value : throw new InvalidOperationException("This option is empty.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
    }
}
