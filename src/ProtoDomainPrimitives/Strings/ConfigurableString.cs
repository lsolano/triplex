using System;
using Triplex.ProtoDomainPrimitives.Exceptions;
using Triplex.ProtoDomainPrimitives.Numerics;

namespace Triplex.ProtoDomainPrimitives.Strings
{
    public sealed class ConfigurableString : IDomainPrimitive<string>
    {
        private readonly StringComparison _comparisonStrategy;

        public string Value => throw new NotImplementedException();

        public int CompareTo(IDomainPrimitive<string> other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IDomainPrimitive<string> other)
        {
            throw new NotImplementedException();
        }

        public  sealed class Builder
        {
            private bool _built;

            //internal string RawValue { get; private set; }
            internal Message ArgumentNullErrorMessage { get; private set; }
            internal Message TooLongErrorMessage { get; private set; }
            internal StringLength MinLength { get; private set; }
            internal StringLength MaxLength { get; private set; }
            internal Message TooShortErrorMessage { get; private set; }
            internal StringComparison ComparisonStrategy { get; private set; } = StringComparison.InvariantCulture;

            public Builder(Message argumentNullErrorMessage)
            {
                ArgumentNullErrorMessage = argumentNullErrorMessage ??
                                           throw new ArgumentNullException(nameof(argumentNullErrorMessage));
            }

            public Builder WithMinLength(StringLength minLength, Message tooShortErrorMessage)
            {
                return DoAndReturn(() =>
                {
                    TooShortErrorMessage = tooShortErrorMessage ?? throw new ArgumentNullException(nameof(tooShortErrorMessage));
                    MinLength = minLength ?? throw new ArgumentNullException(nameof(minLength));
                });
            }

            public Builder WithMaxLength(StringLength maxLength, Message tooLongErrorMessage)
            {
                return DoAndReturn(() =>
                {
                    TooLongErrorMessage = tooLongErrorMessage ?? throw new ArgumentNullException(nameof(tooLongErrorMessage));
                    MaxLength = maxLength ?? throw new ArgumentNullException(nameof(maxLength));
                });
            }

            public Builder WithLengthRange(StringLengthRange lengthRange, Message tooShortErrorMessage, Message tooLongErrorMessage)
            {
                return DoAndReturn(() =>
                {
                    if (lengthRange == null)
                    {
                        throw new ArgumentNullException(nameof(lengthRange));
                    }

                    TooShortErrorMessage = tooShortErrorMessage ?? throw new ArgumentNullException(nameof(tooShortErrorMessage));
                    TooLongErrorMessage = tooLongErrorMessage ?? throw new ArgumentNullException(nameof(tooLongErrorMessage));
                    MinLength = lengthRange.Min;
                    MaxLength = lengthRange.Max;
                });
            }

            /// <summary>
            /// If not set, defaults to <see cref="StringComparison.InvariantCulture"/>.
            /// </summary>
            /// <param name="comparisonStrategy"></param>
            /// <returns></returns>
            public Builder WithComparisonStrategy(StringComparison comparisonStrategy)
            {
                return DoAndReturn(() =>
                {
                    bool notDefined = Enum.IsDefined(typeof(StringComparison), comparisonStrategy);
                    if (notDefined)
                    {
                        throw new ArgumentOutOfRangeException(nameof(comparisonStrategy), comparisonStrategy, $"Value is not a member of enum {nameof(StringComparison)}.");
                    }

                    ComparisonStrategy = comparisonStrategy;
                });
            }

            /*
             * Priority:
             * -4 EnsureTrimmed() => Allow-Trailing-WhiteSpace(false); Allow-Leading-WhiteSpace(false)
             * -3 Allow-Trailing-WhiteSpace(bool)
             * -2 Allow-Leading-WhiteSpace(bool)
             * -1 Allow-WhiteSpace-Only(bool)
             * 0- With-InvalidChars-Regex|Pattern
             * 1- With-ValidFormat-Regex|Pattern
             * 2- With-CustomParser Action(rawValue: string) : void
             */

            private static void EnsureValidLengthRange(StringLength minLength, StringLength maxLength)
            {
                _ = new StringLengthRange(minLength, maxLength);
            }

            private Builder DoAndReturn(Action checkAndSet)
            {
                EnsureNotBuilt();

                checkAndSet();

                return this;
            }

            private void EnsureNotBuilt()
            {
                if (_built)
                {
                    throw new InvalidOperationException("Already built.");
                }
            }
        }
    }
}
