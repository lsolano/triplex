using System;
using Triplex.ProtoDomainPrimitives.Numerics;

namespace Triplex.ProtoDomainPrimitives.Strings
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TrimmedCaseInsensitiveString : AbstractDomainPrimitive<string>, IBoundedLengthString, ICaseInsensitiveString, ITrimmedString
    {
        private readonly Builder _builder;

        private TrimmedCaseInsensitiveString(Builder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// 
        /// </summary>
        public StringLength MinLength => _builder.MinLength;

        /// <summary>
        /// 
        /// </summary>
        public StringLength MaxLength => _builder.MaxLength;

        /// <summary>
        /// 
        /// </summary>
        public new string Value => _builder.Value;

        /// <summary>
        /// 
        /// </summary>
        public sealed class Builder
        {
            private bool _built;

            internal string Value { get; private set; } = string.Empty;
            internal StringLength MinLength { get; private set; } = StringLength.Min;
            internal StringLength MaxLength { get; private set; } = StringLength.Max;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public Builder WithValue(string value)
            {
                EnsuresAcceptsChanges();

                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (HasLeadingOrTrailingWhiteSpaces(value))
                {
                    throw new FormatException($"{nameof(value)} can not have leading or trailing white-spaces.");
                }

                Value = value;

                return this;
            }

            private static bool HasLeadingOrTrailingWhiteSpaces(string value)
            {
                return value.Length > 0 && char.IsWhiteSpace(value, 0) || char.IsWhiteSpace(value, value.Length - 1);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="minLength"></param>
            /// <returns></returns>
            public Builder WithMinLength(StringLength minLength)
            {
                EnsuresAcceptsChanges();

                MinLength = minLength ?? throw new ArgumentNullException(nameof(minLength));

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="maxLength"></param>
            /// <returns></returns>
            public Builder WithMaxLength(StringLength maxLength)
            {
                EnsuresAcceptsChanges();

                MaxLength = maxLength ?? throw new ArgumentNullException(nameof(maxLength));

                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public TrimmedCaseInsensitiveString Build()
            {
                EnsuresAcceptsChanges();

                EnsureNoneIsNull();

                EnsureLength();

                _built = true;
                return new TrimmedCaseInsensitiveString(this);
            }

            private void EnsuresAcceptsChanges()
            {
                if (_built)
                {
                    throw new InvalidOperationException("Already built.");
                }
            }

            private void EnsureNoneIsNull()
            {
                if (Value == null)
                {
                    throw new ArgumentException(nameof(Value));
                }

                if (MinLength == null)
                {
                    throw new ArgumentException(nameof(MinLength));
                }

                if (MaxLength == null)
                {
                    throw new ArgumentException(nameof(MaxLength));
                }
            }

            private void EnsureLength()
            {
                if (MinLength.CompareTo(MaxLength) > 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinLength), MinLength, $"Must be less or equals to {nameof(MaxLength)} ({MaxLength}).");
                }

                int length = Value.Length;
                if (NotBetween(length, MinLength, MaxLength))
                {
                    throw new ArgumentOutOfRangeException(nameof(Value.Length), length, $"Length must be between [{MinLength},{MaxLength}].");
                }
            }

            private static bool NotBetween(int length, StringLength minLength, StringLength maxLength)
                => length < minLength.Value || length > maxLength.Value;
        }
    }
}
