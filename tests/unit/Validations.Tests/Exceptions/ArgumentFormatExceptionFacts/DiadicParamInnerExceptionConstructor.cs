using Triplex.Validations.Exceptions;
using DataSourceType = Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts.TriadicConstructorFacts;

namespace Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts;

[TestFixture]
internal sealed class DiadicParamInnerExceptionConstructor
{
    [Test]
    public void Throws_Nothing(
        [ValueSource(typeof(DataSourceType), nameof(DataSourceType.ValidParameterNames))] string? paramName,
        [ValueSource(typeof(DataSourceType), nameof(DataSourceType.ValidInnerExceptions))] Exception? innerException)
        => Assert.That(() => new ArgumentFormatException(paramName!, innerException!), Throws.Nothing);
}
