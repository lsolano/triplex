using Triplex.Validations.ArgumentsHelpers;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Algorithms.Checksum
{
    /// <summary>
    /// Implementation of the Luhn algorithm in its two variants: as validation and as checksum generator.
    /// </summary>
    public static class LuhnFormula
    {
        private const int MinimumElements = 2;
        private static readonly Regex DigitsRegex = new("^[0-9]+$", RegexOptions.Compiled);

        /// <summary>
        /// Indicates is a sequence of numbers is valid using the Luhn formula.
        /// </summary>
        /// <param name="fullDigits">Can not be <see langword="null"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="fullDigits"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="fullDigits"/> has less than two(2) digits.
        /// </exception>
        /// <exception cref="FormatException">
        /// If <paramref name="fullDigits"/> contains elements not within range [0-9].
        /// </exception>
        public static bool IsValid([ValidatedNotNull] in int[]? fullDigits)
        {
            int[] validatedDigits = fullDigits.ValueOrThrowIfNullOrWithLessThanElements(MinimumElements,
                nameof(fullDigits));
            
            bool hasInvalidElements = validatedDigits.Any(d => d < 0 || d > 9);
            if (hasInvalidElements) {
                throw new FormatException("Only values between zero and nine ( [0-9] ) are allowed as input.");
            }

            return DoDigitCheck(validatedDigits);
        }

        /// <summary>
        /// Indicates the given string (containing a sequence of numbers) is valid using the Luhn formula.
        /// </summary>
        /// <param name="fullDigits">Can not be <see langword="null"/> or empty. Must contains digits only (0-9),
        /// and have a minimum of two (2) elements.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="fullDigits"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="fullDigits"/> has less than two(2) digits.
        /// </exception>
        /// <exception cref="FormatException">
        /// If <paramref name="fullDigits"/> contains characters other than digits.
        /// </exception>
        public static bool IsValid([ValidatedNotNull] in string? fullDigits)
        {
            string notNullDigits = ValidateDigitsAsString(fullDigits);

            int[] validatedDigits = notNullDigits.Select(ch => ch - '0').ToArray();

            return DoDigitCheck(validatedDigits);
        }

        private static string ValidateDigitsAsString([ValidatedNotNull] in string? fullDigits)
        {
            string notNullDigits = fullDigits.ValueOrThrowIfNullOrZeroLength(nameof(fullDigits));

            if (notNullDigits.Length < MinimumElements)
            {
                throw new ArgumentOutOfRangeException(nameof(fullDigits),
                    $"Length must be at least {MinimumElements} elements.");
            }

            if (!DigitsRegex.IsMatch(notNullDigits))
            {
                throw new FormatException("Invalid input, only digits [0-9] are allowed.");
            }

            return notNullDigits;
        }

        private static bool DoDigitCheck(in int[] sanitizedDigits)
        {
            int check = CalculateCheck(sanitizedDigits, isFullSequence: true);

            int lastDigit = sanitizedDigits[^1];

            return check == lastDigit;
        }

        /// <summary>
        /// Calculates the check-digit for the given sequence.
        /// </summary>
        /// <param name="digitsWithoutCheck">Can not be <see langword="null"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="digitsWithoutCheck"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="digitsWithoutCheck"/> is has less than one digits.
        /// </exception>
        public static int GetCheckDigit([ValidatedNotNull] in int[]? digitsWithoutCheck)
        {
            const int minimumElements = 1;

            int[] validatedDigits = digitsWithoutCheck.ValueOrThrowIfNullOrWithLessThanElements(minimumElements,
                nameof(digitsWithoutCheck));

            return CalculateCheck(validatedDigits, isFullSequence: false);
        }

        private static int CalculateCheck(in int[] digits, in bool isFullSequence) {
            int offset = isFullSequence ? 2 : 1;

            int sum = 0;
            for (int i = digits.Length - offset, c = 0; i >= 0; i--, c++)
            {
                int digit = digits[i];

                if (c % 2 == 0)
                {
                    digit *= 2;
                    
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
            }

            return (10 - (sum % 10)) % 10;
        }
    }
}
