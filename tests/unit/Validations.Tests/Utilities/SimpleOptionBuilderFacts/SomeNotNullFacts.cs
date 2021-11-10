using NUnit.Framework;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.SimpleOptionBuilderFacts
{
    [TestFixture]
    internal sealed class SomeNotNullFacts
    {
        [Test]
        public void With_Null_Returns_Empty()
        {
            string? value = null;
            SimpleOption<string> option = SimpleOption.SomeNotNull(value!);

            Assert.That(option.HasValue, Is.False);
        }

        [Test]
        public void With_Non_Null_Returns_Full()
        {
            string? value = "Hello Options";
            SimpleOption<string> option = SimpleOption.SomeNotNull(value!);

            Assert.That(option.HasValue, Is.True);
        }
    }
}
