using System;

namespace Triplex.ProtoDomainPrimitives.Numerics
{
    public sealed class StringLengthRange
    {
        public StringLengthRange(StringLength min, StringLength max)
        {
            Validate(min, max);

            (Min, Max) = (min, max);
        }

        public StringLength Min { get; }
        public StringLength Max { get; }

        private static void Validate(StringLength min, StringLength max)
        {
            if (min == null)
            {
                throw new ArgumentNullException(nameof(min));
            }

            if (max == null)
            {
                throw new ArgumentNullException(nameof(max));
            }

            if (min.CompareTo(max) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, $"{nameof(min)} must be less than or equals to (<=) {nameof(max)} ({max}).");
            }
        }
    }
}
