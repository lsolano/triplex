using System;
using NUnit.Framework;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class ValidLuhnChecksumMessageFacts
    {
        private const string CustomMessage = "Look caller: the sequence is not valid.";

        [Test]
        public void With_Null_Value_Throws_ArgumentNullException()
        {
            string? someLuhnSequence = null;
            Assert.That(() => Arguments.ValidLuhnChecksum(someLuhnSequence, nameof(someLuhnSequence), CustomMessage),
                Throws.ArgumentNullException);
        }

        [TestCase("12345678903")]
        [TestCase("12345678911")]
        [TestCase("12345678929")]
        [TestCase("12345678937")]
        [TestCase("12345678945")]
        [TestCase("12345678952")]
        public void With_Valid_Values_Throws_Nothing(string? value)
        {
            string validatedValue = Arguments.ValidLuhnChecksum(value, nameof(value), CustomMessage);

            Assert.That(validatedValue, Is.EqualTo(value));
        }

        [TestCase("12345678904")]
        [TestCase("12345678912")]
        [TestCase("12345678920")]
        [TestCase("12345678938")]
        [TestCase("12345678946")]
        [TestCase("12345678953")]
        public void With_Invalid_Value_Throws_FormatException(string? value)
        {
            string? copy = value;
            Assert.That(() => Arguments.ValidLuhnChecksum(copy, nameof(value), CustomMessage),
                Throws.InstanceOf<FormatException>()
                    .With.Message.EqualTo(CustomMessage));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        [TestCase("5")]
        public void With_Invalid_Formatted_Value_Throws_FormatException(string? value)
            => Assert.That(() => Arguments.ValidLuhnChecksum(value, nameof(value), CustomMessage),
                Throws.InstanceOf<FormatException>());
    }
}
