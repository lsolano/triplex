namespace Triplex.Validations.Tests.StateFacts;

[TestFixture]
internal sealed class IsNotNullMessageFacts
{
    [Test]
    public void With_Null_Message_Throws_ArgumentNullException([Values("Some", null!)] object stateElement)
    {
        const string argumentName = "elementName";
        Assert.That(() => State.IsNotNull(stateElement, elementName: null!),
            Throws.ArgumentNullException
                  .With.Message.Contains(argumentName)
                  .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo(argumentName));
    }

    [Test]
    public void With_Not_Null_StateElement_And_Not_Null_Message_Throws_Nothing()
        => Assert.That(() => State.IsNotNull("Some", "Message when null."), Throws.Nothing);

    [Test]
    public void With_Null_StateElement_And_Not_Null_Name_Throws_ArgumentNullException()
    {
        object someThing = null!;

        Assert.That(() => State.IsNotNull(someThing, nameof(someThing)),
            Throws.InvalidOperationException.With.Message.Contains(nameof(someThing)));
    }

    [Test]
    public void With_Null_StateElement_And_Without_Name_Throws_ArgumentNullException()
    {
        object someThing = null!;

        Assert.That(() => State.IsNotNull(someThing),
            Throws.InvalidOperationException.With.Message.Contains(nameof(someThing)));
    }
}
