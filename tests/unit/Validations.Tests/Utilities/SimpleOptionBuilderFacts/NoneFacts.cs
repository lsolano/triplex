using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.SimpleOptionBuilderFacts
{
    [TestFixture]
    internal sealed class NoneFacts
    {
        [Test]
        public void Returns_Empty()
        {
            SimpleOption<string> option = SimpleOption.None<string>();

            Assert.That(option.HasValue, Is.False);
        }
    }
}
