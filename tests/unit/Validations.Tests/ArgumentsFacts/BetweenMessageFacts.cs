using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    internal sealed class BetweenMessageFacts : BaseFixtureForOptionalCustomMessage
    {
        private const string CustomMessage = "Look caller: the number is not in range.";
        private const string DefaultParameterName = "length";

        public BetweenMessageFacts(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            => Assume.That(useCustomErrorMessage, Is.True);

        [TestCase(-1, -1, 1)]
        [TestCase(-1, 0, 1)]
        [TestCase(-1, 1, 1)]
        [TestCase(-3, -2, -1)]
        [TestCase(-3, -2, -1)]
        [TestCase(-3, -1, -1)]
        [TestCase(1, 1, 3)]
        [TestCase(1, 2, 3)]
        [TestCase(1, 3, 3)]
        public void With_Valid_Integers_Throws_Nothing(in int from, in int value, in int to)
        {
            int validatedValue = Arguments.Between(value, from, to, nameof(value), CustomMessage);

            Assert.That(validatedValue, Is.EqualTo(value));
        }

        [TestCase(-1, -1, -1)]
        [TestCase(0, -1, -2)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 0)]
        public void With_Invalid_Range_Throws_ArgumentOutOfRangeException(int from, int value, int to) {
            Assert.That(() => Arguments.Between(value, from, to, nameof(value), CustomMessage),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ParamName)).EqualTo("min"));
        }

        [TestCase(-3, -4, -1)]
        [TestCase(-3, 0, -1)]
        [TestCase(-2, -3, 0)]
        [TestCase(-2, 1, 0)]
        [TestCase(0, -1, 2)]
        [TestCase(0, 3, 2)]
        [TestCase(-1, -2, 1)]
        [TestCase(-1, 2, 1)]
        public void With_Invalid_Value_Throws_ArgumentOutOfRangeException(int from, int value, int to) {
            Assert.That(() => Arguments.Between(value, from, to, nameof(value), CustomMessage),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ParamName)).EqualTo(nameof(value))
                    .And.Message.StartsWith(CustomMessage));
        }
    }
}
