using System;
using System.Collections.Generic;
using NUnit.Framework;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.ComparableRangeFactoryFacts
{
    [TestFixture]
    internal sealed class WithBothInclusiveMessageFacts
    {
        private static IEnumerable<TestCaseData> SomeNull()
        {
            const string min = "a";
            const string max = "b";

            yield return new TestCaseData(null, null);
            yield return new TestCaseData(null, max);
            yield return new TestCaseData(min, null);
        }

        [TestCaseSource(nameof(SomeNull))]
        public void With_Some_Null_Throws_ArgumentNullException(string min, string max)
            => Assert.That(() => ComparableRangeFactory.WithBothInclusive(min, max), Throws.ArgumentException);

        [Test]
        public void With_Valid_Range_Throws_Nothing()
            => Assert.That(() => ComparableRangeFactory.WithBothInclusive(1, 2), Throws.Nothing);

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void With_Inverted_Or_Closed_Range_Throws_ArgumentOutOfRangeException(int min, int max)
            => Assert.That(() => ComparableRangeFactory.WithBothInclusive(min, max),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
    }
}
