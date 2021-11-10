using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class NotNullEmptyOrWhiteSpaceOnly_Facts
{
    private const string CustomMessage = "Some error message, caller provided.";


    [Test]
    public void With_Null_CustomError_Throws_ArgumentNullException()
    {
        string? dummyParam = "Hello world!";

        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam), null!),
            Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
    }

    [Test]
    public void With_Empty_CustomError_Throws_ArgumentOutOfRangeException()
    {
        string? dummyParam = "Hello world!";

        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam), string.Empty),
            Throws.InstanceOf<ArgumentOutOfRangeException>()
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
    }

    [TestCase(" ")]
    [TestCase("\n")]
    [TestCase("\r")]
    [TestCase("\t")]
    [TestCase("\n\r\t ")]
    public void With_Common_White_Space_CustomError_Throws_ArgumentFormatException(in string? someErrorMessage)
    {
        string? dummyParam = "Hello world!";
        string? someErrorMessageCopy = someErrorMessage;

        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam), someErrorMessageCopy!),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentException.ParamName)).EqualTo("customMessage"));
    }

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        string? dummyParam = null;

        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam), CustomMessage),
            Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [Test]
    public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
    {
        string? dummyParam = string.Empty;

        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam), CustomMessage),
            Throws.InstanceOf<ArgumentOutOfRangeException>()
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [TestCase(" ")]
    [TestCase("\n")]
    [TestCase("\r")]
    [TestCase("\t")]
    [TestCase("\n\r\t ")]
    public void With_Common_White_Space_Value_Throws_ArgumentFormatException(in string? dummyParam)
    {
        string? dummyParamValue = dummyParam;
        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParamValue, nameof(dummyParam), CustomMessage),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [TestCase("peter")]
    [TestCase("parker ")]
    [TestCase(" Peter Parker Is Spiderman ")]
    public void With_Valid_Values_Returns_Input_Value(in string? someValue)
    {
        string validatedValue = Arguments.NotNullEmptyOrWhiteSpaceOnly(someValue, nameof(someValue), CustomMessage);

        Assert.That(validatedValue, Is.SameAs(someValue));
    }

    [Test]
    public void With_Null_ParamName_Throws_ArgumentNullException()
    {
        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly("dummyValue", null!, CustomMessage),
            Throws.ArgumentNullException
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
    }

    [Test]
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
    {
        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly("dummyValue", string.Empty, CustomMessage),
            Throws.InstanceOf<ArgumentOutOfRangeException>()
            .With.Property(nameof(ArgumentOutOfRangeException.ParamName)).EqualTo("paramName")
            .And.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(0));
    }

    [TestCase(" ")]
    [TestCase("\n")]
    [TestCase("\r")]
    [TestCase("\t")]
    [TestCase("\n\r\t ")]
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException(in string? paramName)
    {
        string? paramNameValue = paramName;
        Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly("dummyValue", paramNameValue!, CustomMessage),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
    }
}
