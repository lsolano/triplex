using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class NotEmptyOrWhiteSpaceOnly_Triadic_MessageFacts
{
    private const string CustomMessage = "Some error message, caller provided.";
    private const string FakeParameterName = "veryFakeParameterName3";

    [Test]
    public void With_Null_CustomMessage_Throws_ArgumentNullException([Values] bool useCustomParamName)
    {
        string? dummyParam = "Hello world!";
        string finalParamName = useCustomParamName ? FakeParameterName : nameof(dummyParam);

        Action act = useCustomParamName ?
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParam, null!, finalParamName) :
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParam, null!);

        Assert.That(() => act(), Throws.ArgumentNullException
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
    }

    [Test]
    public void With_Empty_CustomMessage_Throws_ArgumentFormatException()
    {
        string? dummyParam = "Hello world!";

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParam, string.Empty, nameof(dummyParam)),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("customMessage"));
    }

    [TestCase(" ")]
    [TestCase("\n")]
    [TestCase("\r")]
    [TestCase("\t")]
    [TestCase("\n\r\t ")]
    public void With_Common_White_Space_CustomMessage_Throws_ArgumentFormatException(string? someErrorMessage)
    {
        string? dummyParam = "Hello world!";
        string? someErrorMessageCopy = someErrorMessage;

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParam, someErrorMessageCopy!, nameof(dummyParam)),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentException.ParamName)).EqualTo("customMessage"));
    }

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        string? dummyParam = null;

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParam, CustomMessage, nameof(dummyParam)),
            Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                                             .EqualTo(nameof(dummyParam)));
    }

    [Test]
    public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
    {
        string? dummyParam = string.Empty;

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParam, CustomMessage, nameof(dummyParam)),
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
        string? dummyParamValue = dummyParam;
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(dummyParamValue, CustomMessage, nameof(dummyParam)),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [TestCase("peter")]
    [TestCase("parker ")]
    [TestCase(" Peter Parker Is Spiderman ")]
    public void With_Valid_Values_Returns_Input_Value(string? someValue)
    {
        string validatedValue = Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage(someValue, nameof(someValue), CustomMessage);

        Assert.That(validatedValue, Is.SameAs(someValue));
    }

    [Test]
    public void With_Null_ParamName_Throws_ArgumentNullException()
    {
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage("dummyValue", CustomMessage, null!),
            Throws.ArgumentNullException
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
    }

    [Test]
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
    {
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage("dummyValue", CustomMessage, string.Empty),
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
        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrExceptionWithMessage("dummyValue", CustomMessage, paramNameValue!),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
    }
}
