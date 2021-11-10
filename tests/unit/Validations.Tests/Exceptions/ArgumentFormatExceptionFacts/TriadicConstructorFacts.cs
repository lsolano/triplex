using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.Exceptions.ArgumentFormatExceptionFacts;

[TestFixture]
internal sealed class TriadicConstructorFacts
{
    internal static readonly string?[] ValidParameterNames
        = new string?[] { null, "", " ", "i'm a weird param name", "firstname" };

    internal static readonly string?[] ValidMessages
        = new string?[] { null, "", " ", "Some error message" };

    internal static readonly Exception?[] ValidInnerExceptions
        = new Exception?[] { null, new Exception() };

    [Test]
    public void Throws_Nothing(
        [ValueSource(nameof(ValidParameterNames))] string? paramName,
        [ValueSource(nameof(ValidMessages))] string? message,
        [ValueSource(nameof(ValidInnerExceptions))] Exception? innerException)
        => Assert.That(() => new ArgumentFormatException(paramName!, message!, innerException!), Throws.Nothing);
}
