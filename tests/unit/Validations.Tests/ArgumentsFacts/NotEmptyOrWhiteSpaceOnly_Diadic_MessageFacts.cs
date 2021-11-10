using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class NotEmptyOrWhiteSpaceOnly_Diadic_MessageFacts
{
    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        string? dummyParam = null;

        Assert.That(() => Arguments.NotEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam)),
            Throws.ArgumentNullException.With
                                        .Property(nameof(ArgumentNullException.ParamName))
                                        .EqualTo(nameof(dummyParam)));
    }

    [Test]
    public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
    {
        string? dummyParam = string.Empty;

        Assert.That(() => Arguments.NotEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam)),
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
        Assert.That(() => Arguments.NotEmptyOrWhiteSpaceOnly(dummyParamValue, nameof(dummyParam)),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(dummyParam)));
    }

    [TestCase("peter")]
    [TestCase("parker ")]
    [TestCase(" Peter Parker Is Spiderman ")]
    public void With_Valid_Values_Returns_Input_Value(in string? someValue)
    {
        string validatedValue = Arguments.NotEmptyOrWhiteSpaceOnly(someValue, nameof(someValue));

        Assert.That(validatedValue, Is.SameAs(someValue));
    }

    [Test]
    public void With_Null_ParamName_Throws_ArgumentNullException()
    {
        Assert.That(() => Arguments.NotEmptyOrWhiteSpaceOnly("dummyValue", null!),
            Throws.ArgumentNullException
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
    }

    [Test]
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
    {
        Assert.That(() => Arguments.NotEmptyOrWhiteSpaceOnly("dummyValue", string.Empty),
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
        Assert.That(() => Arguments.NotEmptyOrWhiteSpaceOnly("dummyValue", paramNameValue!),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
    }
}
