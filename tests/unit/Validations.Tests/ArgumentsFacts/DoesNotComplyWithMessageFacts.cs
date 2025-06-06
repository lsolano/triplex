namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class DoesNotComplyWithMessageFacts
{
    private const string PreconditionDescription = "Must be true.";

    [Test]
    public void With_False_Throws_Nothing()
    {
        const string? someString = "Hello World!";
        Assert.That(() => Arguments.DoesNotComplyWith(someString, val => val.Length == 0, nameof(someString),
            PreconditionDescription), Throws.Nothing);
    }

    [Test]
    public void Returns_Same_Instance()
    {
        const string? someString = "Hello World 85";
        string notNullValue = Arguments.DoesNotComplyWith(someString, val => val.Length == 0, nameof(someString),
            PreconditionDescription);

        Assert.That(notNullValue, Is.SameAs(someString));
    }

    [Test]
    public void With_True_Throws_ArgumentException()
    {
        const string? someString = "123456789";
        Assert.That(() => Arguments.DoesNotComplyWith(someString, val => val.Length == 9, PreconditionDescription,
        nameof(someString)),
            Throws.ArgumentException
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(someString))
                  .And.Message.Contains(PreconditionDescription));
    }

    [Test]
    public void With_Invalid_ParamName_Throws_ArgumentException([Values(null, "", " ", "\n\r\t ")] string? paramName,
        [Values] bool precondition)
    {
        const string? someString = "Hello World 1235";

        Assert.That(() => Arguments.DoesNotComplyWith(someString, val => precondition,
            PreconditionDescription, paramName!),
            Throws.InstanceOf<ArgumentException>()
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo("paramName"));
    }

    [Test]
    public void With_Invalid_Description_ParamName_Throws_ArgumentException(
        [Values(null, "", " ", "\n\r\t ")] string? description, [Values] bool precondition)
    {
        const string? someString = "Hello World 1235";

        Assert.That(() => Arguments.DoesNotComplyWith(someString, val => precondition, description!, nameof(someString)),
            Throws.InstanceOf<ArgumentException>()
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo("preconditionDescription"));
    }

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        const string? someString = null;
        Assert.That(() => Arguments.DoesNotComplyWith(someString, val => val.Length > 100,
            PreconditionDescription, nameof(someString)),
            Throws.ArgumentNullException
                  .With.Property(nameof(ArgumentException.ParamName)).EqualTo("value")
                  .And.Message.Contains("cannot be null"));
    }
}
