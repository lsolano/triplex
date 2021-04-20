using System;
using Triplex.Validations.ArgumentsHelpers;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Algorithms.Checksum
{
    /// <summary>
    /// Implementation of the Luhn algorith in its two variants: as validation and as checksum generator.
    /// </summary>
    public static class LuhnFormula
    {
        /// <summary>
        /// Indicates is a sequence of numbers is valid using the Luhn formula.
        /// </summary>
        /// <param name="fullDigits">Can not be <see langword="null"/></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">If <paramref name="fullDigits"/> is <see langword="null"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If <paramref name="fullDigits"/> is has less than two digits.</exception>
        public static bool IsValid([ValidatedNotNull] in int[] fullDigits) {
            const int minimumElements = 2;

            int[] validatedDigits = fullDigits.ValueOrThrowIfNullOrWithLessThanElements(minimumElements, nameof(fullDigits));
          
            int check = CalculateCheck(validatedDigits, isFullSequence: true);

            int lastDigit = validatedDigits[validatedDigits.Length - 1];

            return check == lastDigit;
        }

        /// <summary>
        /// Calculates the check-digit for the given sequence.
        /// </summary>
        /// <param name="digitsWithoutCheck">Can not be <see langword="null"/></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">If <paramref name="digitsWithoutCheck"/> is <see langword="null"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If <paramref name="digitsWithoutCheck"/> is has less than one digits.</exception>
        public static int GetCheckDigit([ValidatedNotNull] in int[] digitsWithoutCheck)
        {
            const int minimumElements = 1;

            int[] validatedDigits = digitsWithoutCheck.ValueOrThrowIfNullOrWithLessThanElements(minimumElements, nameof(digitsWithoutCheck));

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
