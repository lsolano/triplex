namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class CompliesWithUsingLambdaMessageFacts
{
    private const string PreconditionDescription = "Must be true.";

    [Test]
    public void With_True_Throws_Nothing()
    {
        const string? someString = "Hello World!";
        Assert.That(() => Arguments.CompliesWith(someString, val => val.Length > 2, nameof(someString),
            PreconditionDescription), Throws.Nothing);
    }

    [Test]
    public void Returns_Same_Instance()
    {
        const string? someString = "Hello World 85";
        string notNullValue = Arguments.CompliesWith(someString, val => val.Length > 2, nameof(someString),
            PreconditionDescription);

        Assert.That(notNullValue, Is.SameAs(someString));
    }

    [Test]
    public void With_False_Throws_ArgumentException()
    {
        const string? someString = "Hello World 1234";
        Assert.That(() => Arguments.CompliesWith(someString, val => val.Length > 100, nameof(someString),
            PreconditionDescription),
            Throws.ArgumentException
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(someString))
                  .And.Message.Contains(PreconditionDescription));
    }

    [Test]
    public void With_Invalid_ParamName_Throws_ArgumentException([Values(null, "", " ", "\n\r\t ")] string? paramName,
        [Values] bool precondition)
    {
        const string? someString = "Hello World 1235";

        Assert.That(() => Arguments.CompliesWith(someString, val => precondition, paramName!, PreconditionDescription),
            Throws.InstanceOf<ArgumentException>()
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo("paramName"));
    }

    [Test]
    public void With_Invalid_Description_ParamName_Throws_ArgumentException(
        [Values(null, "", " ", "\n\r\t ")] string? description, [Values] bool precondition)
    {
        const string? someString = "Hello World 1235";

        Assert.That(() => Arguments.CompliesWith(someString, val => precondition, nameof(someString), description!),
            Throws.InstanceOf<ArgumentException>()
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo("preconditionDescription"));
    }

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        const string? someString = null;
        Assert.That(() => Arguments.CompliesWith(someString, val => val.Length > 100, nameof(someString),
            PreconditionDescription),
            Throws.ArgumentNullException
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo("value")
                  .And.Message.Contains("cannot be null"));
    }
}
