using NUnit.Framework;
using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts
{
    [TestFixture]
    internal sealed class NiladicConstructorFacts
    {
        [Test]
        public void Throws_Nothing() => Assert.That(() => new ArgumentFormatException(), Throws.Nothing);
    }
}
