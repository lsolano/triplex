using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class ValidLuhnChecksumMessageFacts
    {
        private const string CustomMessage = "Look caller: the sequence is not valid.";

        [TestCase("12345678903")]
        [TestCase("12345678911")]
        [TestCase("12345678929")]
        [TestCase("12345678937")]
        [TestCase("12345678945")]
        [TestCase("12345678952")]
        public void With_Valid_Values_Throws_Nothing(in string? value)
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
        public void With_Invalid_Value_Throws_FormatException(in string? value) {
            string? copy = value;
            Assert.That(() => Arguments.ValidLuhnChecksum(copy, nameof(value), CustomMessage),
                Throws.InstanceOf<FormatException>()
                    .With.Message.EqualTo(CustomMessage));
        }
    }
}
