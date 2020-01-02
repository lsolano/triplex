using System;
using System.Linq;
using System.Text.RegularExpressions;
using Triplex.ProtoDomainPrimitives.Exceptions;
using Triplex.ProtoDomainPrimitives.Numerics;

namespace Triplex.ProtoDomainPrimitives.Strings
{
    public sealed class ConfigurableString : IDomainPrimitive<string>
    {
        private readonly StringComparison _comparisonStrategy;

        private ConfigurableString(string rawValue, StringComparison comparisonStrategy) {
            Value = rawValue;
            _comparisonStrategy = comparisonStrategy;
        }

        public string Value { get; }

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
            private static readonly Message DefaultArgumentNullErrorMessage = new Message("Input string can not be null.");
            private static readonly Message DefaultTooShortErrorMessage = new Message("Input string is too short.");
            private static readonly Message DefaultTooLongErrorMessage = new Message("Input string is too long.");
            private static readonly Message DefaultInvalidCharactersErrorMessage = new Message("Input string contains invalid characters.");
            private static readonly Message DefaultInvalidFormatErrorMessage = new Message("Input string has an invalid format.");

            private bool _built;

            internal StringComparison ComparisonStrategy { get; private set; } = StringComparison.InvariantCulture;
            internal Message ArgumentNullErrorMessage { get; private set; } = DefaultArgumentNullErrorMessage;
            internal Message TooShortErrorMessage { get; private set; } = DefaultTooShortErrorMessage;
            internal Message TooLongErrorMessage { get; private set; } = DefaultTooLongErrorMessage;
            internal Message InvalidCharactersErrorMessage { get; private set; } = DefaultInvalidCharactersErrorMessage;
            internal Message InvalidFormatErrorMessage { get; private set; } = DefaultInvalidFormatErrorMessage;

            internal StringLength MinLength { get; private set; }
            internal StringLength MaxLength { get; private set; }
            internal bool RequiresTrimmed { get; private set; }
            internal bool AllowLeadingWhiteSpace { get; private set; } = true;
            private bool DoesNotAllowLeadingWhiteSpace => !AllowLeadingWhiteSpace;
            internal bool AllowTrailingWhiteSpace { get; private set; } = true;
            private bool DoesNotAllowTrailingWhiteSpace => !AllowTrailingWhiteSpace;
            internal bool AllowWhiteSpacesOnly { get; private set; } = true;
            private bool DoesNotAllowWhiteSpacesOnly => !AllowWhiteSpacesOnly;
            internal Regex InvalidCharsRegex { get; private set; }
            internal Regex ValidFormatRegex { get; private set; }

            public Builder(Message argumentNullErrorMessage)
            {
                ArgumentNullErrorMessage = argumentNullErrorMessage ??
                                           throw new ArgumentNullException(nameof(argumentNullErrorMessage));
            }

            /// <summary>
            /// If not set, defaults to <see cref="StringComparison.InvariantCulture"/>.
            /// </summary>
            /// <param name="comparisonStrategy"></param>
            /// <returns></returns>
            public Builder WithComparisonStrategy(StringComparison comparisonStrategy)
            {
                return CheckPreconditionsAndExecute(() =>
                {
                    bool notDefined = Enum.IsDefined(typeof(StringComparison), comparisonStrategy);
                    if (notDefined)
                    {
                        throw new ArgumentOutOfRangeException(nameof(comparisonStrategy), comparisonStrategy, $"Value is not a member of enum {nameof(StringComparison)}.");
                    }

                    ComparisonStrategy = comparisonStrategy;
                });
            }

            public Builder WithMinLength(StringLength minLength, Message tooShortErrorMessage)
            {
                return CheckPreconditionsAndExecute(() =>
                {
                    TooShortErrorMessage = tooShortErrorMessage ?? throw new ArgumentNullException(nameof(tooShortErrorMessage));
                    MinLength = minLength ?? throw new ArgumentNullException(nameof(minLength));
                });
            }

            public Builder WithMaxLength(StringLength maxLength, Message tooLongErrorMessage)
            {
                return CheckPreconditionsAndExecute(() =>
                {
                    TooLongErrorMessage = tooLongErrorMessage ?? throw new ArgumentNullException(nameof(tooLongErrorMessage));
                    MaxLength = maxLength ?? throw new ArgumentNullException(nameof(maxLength));
                });
            }

            public Builder WithLengthRange(StringLengthRange lengthRange, Message tooShortErrorMessage, Message tooLongErrorMessage)
            {
                return CheckPreconditionsAndExecute(() =>
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

            public Builder WithRequiresTrimmed(bool requiresTrimmed)
                => WithRequiresTrimmed(requiresTrimmed, DefaultInvalidFormatErrorMessage);

            public Builder WithRequiresTrimmed(bool requiresTrimmed, Message invalidFormatErrorMessage)
            {
                return CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(invalidFormatErrorMessage, () =>
                {
                    if (requiresTrimmed)
                    {
                        AllowLeadingWhiteSpace = AllowTrailingWhiteSpace = AllowWhiteSpacesOnly = false;
                    }
                });
            }

            public Builder WithAllowLeadingWhiteSpace(bool allowLeadingWhiteSpace)
                => WithAllowLeadingWhiteSpace(allowLeadingWhiteSpace, DefaultInvalidFormatErrorMessage);

            public Builder WithAllowLeadingWhiteSpace(bool allowLeadingWhiteSpace, Message invalidFormatErrorMessage)
            {
                return CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(invalidFormatErrorMessage, () =>
                {
                    AllowLeadingWhiteSpace = allowLeadingWhiteSpace;
                });
            }

            public Builder WithAllowTrailingWhiteSpace(bool allowTrailingWhiteSpace)
                => WithAllowTrailingWhiteSpace(allowTrailingWhiteSpace, DefaultInvalidFormatErrorMessage);

            public Builder WithAllowTrailingWhiteSpace(bool allowTrailingWhiteSpace, Message invalidFormatErrorMessage)
            {
                return CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(invalidFormatErrorMessage, () =>
                {
                    AllowTrailingWhiteSpace = allowTrailingWhiteSpace;
                });
            }

            public Builder WithAllowWhiteSpacesOnly(bool allowWhiteSpacesOnly)
                => WithAllowWhiteSpacesOnly(allowWhiteSpacesOnly, DefaultInvalidFormatErrorMessage);

            public Builder WithAllowWhiteSpacesOnly(bool allowWhiteSpacesOnly, Message invalidFormatErrorMessage)
            {
                return CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(invalidFormatErrorMessage, () =>
                {
                    AllowWhiteSpacesOnly = allowWhiteSpacesOnly;

                    if (allowWhiteSpacesOnly)
                    {
                        AllowLeadingWhiteSpace = AllowTrailingWhiteSpace = true;
                    }
                });
            }

            public Builder WithInvalidCharsPattern(string pattern)
                => WithInvalidCharsPattern(pattern, DefaultInvalidCharactersErrorMessage);

            public Builder WithInvalidCharsPattern(string pattern, Message invalidCharactersErrorMessage)
            {
                return CheckPreconditionsAndExecute(() =>
                {
                    InvalidCharactersErrorMessage = invalidCharactersErrorMessage ?? throw new ArgumentNullException(nameof(invalidCharactersErrorMessage));
                    InvalidCharsRegex = new Regex(
                        pattern ?? throw new ArgumentNullException(nameof(pattern)),
                        RegexOptions.Compiled | RegexOptions.CultureInvariant, Regex.InfiniteMatchTimeout);
                });
            }

            public Builder WithInvalidCharsRegex(Regex regex)
                => WithInvalidCharsRegex(regex, DefaultInvalidCharactersErrorMessage);

            public Builder WithInvalidCharsRegex(Regex regex, Message invalidCharactersErrorMessage)
            {
                return CheckPreconditionsAndExecute(() =>
                {
                    InvalidCharactersErrorMessage = invalidCharactersErrorMessage ?? throw new ArgumentNullException(nameof(invalidCharactersErrorMessage));
                    InvalidCharsRegex = regex ?? throw new ArgumentNullException(nameof(regex));
                });
            }

            public Builder WithValidFormatPattern(string pattern)
                => WithValidFormatPattern(pattern, DefaultInvalidFormatErrorMessage);

            public Builder WithValidFormatPattern(string pattern, Message invalidFormatErrorMessage)
            {
                return CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(invalidFormatErrorMessage, () =>
                {
                    ValidFormatRegex = new Regex(
                        pattern ?? throw new ArgumentNullException(nameof(pattern)),
                        RegexOptions.Compiled | RegexOptions.CultureInvariant, Regex.InfiniteMatchTimeout);
                });
            }

            public Builder WithValidFormatRegex(Regex regex)
                => WithValidFormatRegex(regex, DefaultInvalidFormatErrorMessage);

            public Builder WithValidFormatRegex(Regex regex, Message invalidFormatErrorMessage)
            {
                return CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(invalidFormatErrorMessage, () =>
                {
                    ValidFormatRegex = regex ?? throw new ArgumentNullException(nameof(regex));
                });
            }

            private Builder CheckPreconditionsAndExecute(Action checkAndSet)
            {
                EnsureNotBuilt();

                checkAndSet();

                return this;
            }

            private Builder CheckPreconditionsTrySetInvalidFormatErrorMessageAndExecute(Message invalidFormatErrorMessage, Action checkAndSet)
            {
                EnsureNotBuilt();

                InvalidFormatErrorMessage = invalidFormatErrorMessage ?? throw new ArgumentNullException(nameof(invalidFormatErrorMessage));

                checkAndSet();

                return this;
            }

            public ConfigurableString Build(string rawValue, Action<string> customParser)
            {
                EnsureNotBuilt();

                CheckForNull(rawValue, customParser);

                CheckLengthRange(rawValue);

                CheckForTrimming(rawValue);

                CheckForInvalidChars(rawValue);

                CheckForValidFormat(rawValue);

                CheckForWhiteSpaces(rawValue);

                customParser(rawValue);

                _built = true;

                return new ConfigurableString(rawValue, ComparisonStrategy);
            }

            private void EnsureNotBuilt()
            {
                if (_built)
                {
                    throw new InvalidOperationException("Already built.");
                }
            }

            private void CheckForNull(string rawValue, Action<string> customParser)
            {
                if (customParser == null)
                {
                    throw new ArgumentNullException(nameof(customParser));
                }

                if (rawValue == null)
                {
                    throw new ArgumentNullException(nameof(rawValue), ArgumentNullErrorMessage.Value);
                }
            }

            private void CheckLengthRange(string rawValue)
            {
                StringLengthRange.Validate(MinLength ?? StringLength.Min, MaxLength ?? StringLength.Max);

                if (MinLength != null && rawValue.Length < MinLength.Value)
                {
                    throw new ArgumentOutOfRangeException(nameof(rawValue), rawValue.Length, TooShortErrorMessage.Value);
                }

                if (MaxLength != null && rawValue.Length > MaxLength.Value)
                {
                    throw new ArgumentOutOfRangeException(nameof(rawValue), rawValue.Length, TooLongErrorMessage.Value);
                }
            }

            private void CheckForTrimming(string rawValue)
            {
                bool HasLeadingWhiteSpace() => char.IsWhiteSpace(rawValue, 0);
                bool HasTrailingWhiteSpace() => char.IsWhiteSpace(rawValue, rawValue.Length - 1);

                if (rawValue.Length == 0)
                {
                    return;
                }

                if (RequiresTrimmed)
                {
                    if (HasLeadingWhiteSpace() || HasTrailingWhiteSpace())
                    {
                        throw new FormatException(InvalidFormatErrorMessage.Value);
                    }
                }
                else
                {
                    if (DoesNotAllowLeadingWhiteSpace && HasLeadingWhiteSpace())
                    {
                        throw new FormatException(InvalidFormatErrorMessage.Value);
                    }

                    if (DoesNotAllowTrailingWhiteSpace && HasTrailingWhiteSpace())
                    {
                        throw new FormatException(InvalidFormatErrorMessage.Value);
                    }
                }
            }

            private void CheckForInvalidChars(string rawValue)
            {
                if (InvalidCharsRegex == null)
                {
                    return;
                }

                throw new NotImplementedException();
            }

            private void CheckForValidFormat(string rawValue)
            {
                if (ValidFormatRegex == null)
                {
                    return;
                }

                throw new NotImplementedException();
            }

            private void CheckForWhiteSpaces(string rawValue)
            {
                bool CanNotBeEmpty() => MinLength != null && MinLength.Value > 0;
                bool IsWhiteSpaceOnly() => rawValue.All(ch => char.IsWhiteSpace(ch));

                if (DoesNotAllowWhiteSpacesOnly && CanNotBeEmpty() && IsWhiteSpaceOnly())
                {
                    throw new FormatException(InvalidFormatErrorMessage.Value);
                }
            }
        }
    }
}
