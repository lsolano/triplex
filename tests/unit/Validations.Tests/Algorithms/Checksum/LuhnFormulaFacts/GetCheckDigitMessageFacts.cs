using NUnit.Framework;
using System;
using System.Linq;
using Triplex.Validations.Algorithms.Checksum;

namespace Triplex.Validations.Tests.Algorithms.Checksum.LuhnFormulaFacts
{
    [TestFixture]
    internal sealed class GetCheckDigitMessageFacts
    {
        [Test]
        public void Rejects_Null()
        {
            Assert.That(() => LuhnFormula.GetCheckDigit(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Rejects_Less_Than_One_Elements()
        {
            int[] digits = new int[0];

            Assert.That(() => LuhnFormula.GetCheckDigit(digits), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Accepts_One_Or_More_Elements([Values(1, 2, 3, 5, 8, 13)] int length)
        {
            int[] digits = new int[length];

            Assert.That(() => LuhnFormula.GetCheckDigit(digits), Throws.Nothing);
        }

        [TestCase("0", 0)]
        [TestCase("01", 8)]
        [TestCase("012", 5)]
        [TestCase("0123", 0)]
        [TestCase("01234", 4)]
        [TestCase("012345", 5)]
        [TestCase("0123456", 6)]
        [TestCase("01234567", 4)]
        [TestCase("012345678", 2)]
        [TestCase("0123456789", 7)]
        [TestCase("7992739871", 3)]
        [TestCase("1234567890", 3)]
        [TestCase("1234567891", 1)]
        [TestCase("1234567892", 9)]
        [TestCase("1234567893", 7)]
        [TestCase("1234567894", 5)]
        [TestCase("1234567895", 2)]
        [TestCase("1000000001", 7)]
        [TestCase("1000000002", 5)]
        [TestCase("1000000003", 3)]
        [TestCase("1000000004", 1)]
        [TestCase("1000000005", 8)]
        [TestCase("1000000006", 6)]
        [TestCase("1000000007", 4)]
        [TestCase("1000000008", 2)]
        [TestCase("1000000009", 0)]
        [TestCase("1000000010", 8)]
        public void Returns_True_For_Valid_Sequences(string rawDigits, int expectedCheckDigit)
        {
            int[] digits = rawDigits.Select(ch => int.Parse(ch.ToString())).ToArray();

            Assert.That(LuhnFormula.GetCheckDigit(digits), Is.EqualTo(expectedCheckDigit));
        }
    }
}
