using System.Collections.Generic;
using NUnit.Framework;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.SimpleOptionFacts
{
    [TestFixture]
    internal sealed class GetHashCodeFacts
    {
        [Test]
        public void With_Empty_Returns_Same_Value()
        {
            SimpleOption<string> emptyOption = SimpleOption.SomeNotNull<string>(null!);
            SimpleOption<string> stringOption = SimpleOption.None<string>();
            SimpleOption<int> intOption = SimpleOption.None<int>();

            ISet<int> hashes = new HashSet<int>{
                emptyOption.GetHashCode(), stringOption.GetHashCode(), intOption.GetHashCode()
            };

            Assert.That(hashes.Count, Is.EqualTo(1));
        }

        [Test]
        public void With_Non_Empty_Returns_Same_As_Wrapped_Value([Values("Hello", "World", "Again")] string value)
        {
            SimpleOption<string> stringOption = SimpleOption.SomeNotNull(value);

            Assert.That(stringOption.GetHashCode(), Is.EqualTo(value.GetHashCode()));
        }
    }
}
