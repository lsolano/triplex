using NUnit.Framework;
using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts
{
    [TestFixture]
    internal sealed class MonadicConstructorFacts
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("i'm a weird param name")]
        [TestCase("firstName")]
        public void Throws_Nothing(string? paramName)
            => Assert.That(() => new ArgumentFormatException(paramName!), Throws.Nothing);
    }
}
