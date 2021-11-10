using NUnit.Framework;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.SimpleOptionFacts
{
    [TestFixture]
    internal sealed class ValueOrFailureFacts
    {
        [Test]
        public void With_None_Throws_InvalidOperationException()
        {
            SimpleOption<string> empty = SimpleOption.None<string>();

            Assert.That(() => empty.ValueOrFailure, Throws.InvalidOperationException);
        }

        [Test]
        public void With_Some_Returns_Wrapped_Value()
        {
            const string value = "Hello Options";
            SimpleOption<string> stringOption = SimpleOption.SomeNotNull(value);

            Assert.That(stringOption.ValueOrFailure, Is.SameAs(value));
        }
    }
}
