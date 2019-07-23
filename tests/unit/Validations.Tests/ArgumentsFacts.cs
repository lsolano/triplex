using NUnit.Framework;
using Triplex.Validations;

namespace Triplex.Validations.Tests
{
    [TestFixture]
    internal static class ArgumentsFacts
    {
        [TestFixture]
        internal sealed class NotNullMessageFacts {
            [Test]
            public void With_Null_Throws_ArgumentNullException() {
                Assert.That(() => Arguments.NotNull(null), Throws.ArgumentNullException);
            }

            [Test]
            public void With_Peter_Throws_Nothing() {
                Assert.That(() => Arguments.NotNull("Peter"), Throws.Nothing);
            }
        }
    }
}
