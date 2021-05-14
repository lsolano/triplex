using System;
using System.Linq;

using NUnit.Framework;

using Triplex.Validations.Algorithms.Checksum;

namespace Triplex.Validations.Tests.Algorithms.Checksum.LuhnFormulaFacts
{
    [TestFixture]
    internal sealed class IsValidMessageFacts
    {
        [Test]
        public void Rejects_Null()
            => Assert.That(() => LuhnFormula.IsValid((int[])null), Throws.ArgumentNullException);

        [TestCase("1a01")]
        [TestCase("1 01")]
        [TestCase("1\t01")]
        [TestCase("1-01")]
        [TestCase("1+01")]
        [TestCase("ab")]
        public void With_Non_Digit_Characters_Throws_FormatException(string rawDigits){
            const int zeroAsciiCode = '0';
            int[] digits = rawDigits.Select(ch => ch - zeroAsciiCode).ToArray();

            Assert.That(() => LuhnFormula.IsValid(digits), Throws.InstanceOf<FormatException>());
        }

        [Test]
        public void Rejects_Less_Than_Two_Elements([Values(0, 1)] int length)
        {
            int[] digits = new int[length];

            Assert.That(() => LuhnFormula.IsValid(digits), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Accepts_Two_Or_More_Elements([Values(2, 3, 5, 8, 13)] int length)
        {
            int[] digits = new int[length];

            Assert.That(() => LuhnFormula.IsValid(digits), Throws.Nothing);
        }

        [TestCase("00")]
        [TestCase("018")]
        [TestCase("0125")]
        [TestCase("01230")]
        [TestCase("012344")]
        [TestCase("0123455")]
        [TestCase("01234566")]
        [TestCase("012345674")]
        [TestCase("0123456782")]
        [TestCase("01234567897")]
        [TestCase("79927398713")]

        [TestCase("12345678903")]
        [TestCase("12345678911")]
        [TestCase("12345678929")]
        [TestCase("12345678937")]
        [TestCase("12345678945")]
        [TestCase("12345678952")]

        [TestCase("10000000017")]
        [TestCase("10000000025")]
        [TestCase("10000000033")]
        [TestCase("10000000041")]
        [TestCase("10000000058")]
        [TestCase("10000000066")]
        [TestCase("10000000074")]
        [TestCase("10000000082")]
        [TestCase("10000000090")]
        [TestCase("10000000108")]
        public void Returns_True_For_Valid_Sequences(string rawDigits)
        {
            const int zeroAsciiCode = '0';
            int[] digits = rawDigits.Select(ch => ch - zeroAsciiCode).ToArray();

            Assert.That(LuhnFormula.IsValid(digits), Is.True);
        }

        [TestCase("10000000091")]
        [TestCase("10000000109")]
        public void Returns_False_For_Invalid_Sequences(string rawDigits)
        {
            const int zeroAsciiCode = '0';
            int[] digits = rawDigits.Select(ch => ch - zeroAsciiCode).ToArray();

            Assert.That(LuhnFormula.IsValid(digits), Is.False);
        }

        [TestCase("10000000090")]
        [TestCase("10000000108")]
        public void Returns_True_For_Valid_Sequences_As_String(string rawDigits)
            => Assert.That(LuhnFormula.IsValid(rawDigits), Is.True);

        [TestCase("10000000091")]
        [TestCase("10000000109")]
        public void Returns_False_For_Invalid_Sequences_As_String(string rawDigits)
            => Assert.That(LuhnFormula.IsValid(rawDigits), Is.False);

        [Test]
        public void With_Null_As_String_Throws_ArgumentNullException()
            => Assert.That(() => LuhnFormula.IsValid((string)null), Throws.ArgumentNullException);

        [TestCase("1a01")]
        [TestCase("1 01")]
        [TestCase("1\t01")]
        [TestCase("1-01")]
        [TestCase("1+01")]
        [TestCase("ab")]
        public void With_Non_Digit_Characters_Throws_FormatException_As_String(string rawDigits)
            => Assert.That(() => LuhnFormula.IsValid(rawDigits), Throws.InstanceOf<FormatException>());

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("a")]
        [TestCase("1")]
        public void With_Less_Than_Two_Elements_Throws_ArgumentOutOfRangeException(string rawDigits)
            => Assert.That(() => LuhnFormula.IsValid(rawDigits), Throws.InstanceOf<ArgumentOutOfRangeException>());
    }
}
