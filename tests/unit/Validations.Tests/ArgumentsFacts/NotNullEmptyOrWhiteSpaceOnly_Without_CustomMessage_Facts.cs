using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class NotNullEmptyOrWhiteSpaceOnly_Without_CustomMessage_Facts
{
    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        string? dummyParam = null;

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(dummyParam!, nameof(dummyParam)),
            Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                .EqualTo(nameof(dummyParam)));
    }

    [Test]
    public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
    {
        string? dummyParam = string.Empty;

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(dummyParam, nameof(dummyParam)),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [TestCase(" ")]
    [TestCase("\n")]
    [TestCase("\r")]
    [TestCase("\t")]
    [TestCase("\n\r\t ")]
    public void With_Common_White_Space_Value_Throws_ArgumentFormatException(string? dummyParam)
    {
        string? copy = dummyParam;
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(copy, nameof(dummyParam)),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [TestCase("peter")]
    [TestCase("parker ")]
    [TestCase(" Peter Parker Is Spiderman ")]
    public void With_Valid_Values_Returns_Input_Value(string? someValue)
    {
        string validatedValue = Arguments.NotEmptyNorWhiteSpaceOnlyOrException(someValue, nameof(someValue));

        Assert.That(validatedValue, Is.SameAs(someValue));
    }

    [Test]
    public void With_Null_ParamName_Throws_ArgumentNullException()
    {
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException("dummyValue", null!),
            Throws.ArgumentNullException
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
    }

    [Test]
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
    {
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException("dummyValue", string.Empty),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
    }

    [TestCase(" ")]
    [TestCase("\n")]
    [TestCase("\r")]
    [TestCase("\t")]
    [TestCase("\n\r\t ")]
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException(string? paramName)
    {
        string? paramNameValue = paramName;
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException("dummyValue", paramNameValue!),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
    }
}
