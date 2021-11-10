using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts
{
    [TestFixture]
    internal sealed class DiadicParamInnerExceptionConstructor
    {
        [Test]
        public void Throws_Nothing(
            [ValueSource(typeof(TriadicConstructorFacts), nameof(TriadicConstructorFacts.ValidParameterNames))] string? paramName,
            [ValueSource(typeof(TriadicConstructorFacts), nameof(TriadicConstructorFacts.ValidInnerExceptions))] Exception? innerException)
            => Assert.That(() => new ArgumentFormatException(paramName!, innerException!), Throws.Nothing);
    }
}
