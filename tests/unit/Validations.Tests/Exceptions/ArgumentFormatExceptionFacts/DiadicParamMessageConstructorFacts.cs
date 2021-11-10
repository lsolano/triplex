using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts
{
    [TestFixture]
    internal sealed class DiadicParamMessageConstructorFacts
    {
        [Test]
        public void Throws_Nothing(
            [ValueSource(typeof(TriadicConstructorFacts), nameof(TriadicConstructorFacts.ValidParameterNames))] string? paramName,
            [ValueSource(typeof(TriadicConstructorFacts), nameof(TriadicConstructorFacts.ValidMessages))] string? message)
            => Assert.That(() => new ArgumentFormatException(paramName!, message!), Throws.Nothing);
    }
}
