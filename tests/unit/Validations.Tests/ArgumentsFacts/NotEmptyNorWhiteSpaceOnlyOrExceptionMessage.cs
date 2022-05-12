using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class NotEmptyNorWhiteSpaceOnlyOrExceptionMessage
{
    private const string FakeParameterName = "veryFakeParameterName";

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException([Values] bool useCustomParamName)
    {
        string? dummyParam = null;
        string finalParamName = useCustomParamName ? FakeParameterName : nameof(dummyParam);

        Action act = useCustomParamName ?
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(dummyParam, FakeParameterName) :
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(dummyParam);

        Assert.That(() => act(), Throws.ArgumentNullException.With
                                       .Property(nameof(ArgumentNullException.ParamName))
                                       .EqualTo(finalParamName));
    }

    [Test]
    public void With_Empty_And_Common_White_Space_Values_Throws_ArgumentFormatException(
        [Values("", " ", "\n", "\r", "\t", "\n\r\t")] string? dummyParam, [Values] bool useCustomParamName)
    {
        string finalParamName = useCustomParamName ? FakeParameterName : nameof(dummyParam);

        Action act = useCustomParamName ?
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(dummyParam, FakeParameterName) :
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(dummyParam);

        Assert.That(() => act(), Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo(finalParamName));
    }

    [Test]
    public void With_Valid_Values_Returns_Input_Value(
        [Values("peter", "parker ", " Peter Parker Is Spiderman ")] string someValue, [Values] bool useCustomParamName)
    {
        Func<string> act = BuildCheckForNotNull(someValue, useCustomParamName);

        string validatedValue = act();

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
    public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException(
        [Values("", " ", "\n", "\r", "\t", "\n\r\t")] string? paramName, [Values] bool useValidValue)
    {
        string email = useValidValue ? "peter.parker@marvel.com" : null!;

        Assert.That(() => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(email, paramName!),
            Throws.InstanceOf<ArgumentFormatException>()
            .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
    }

    private static Func<string> BuildCheckForNotNull(string? notNullValue, bool useCustomParamName)
    {
        return useCustomParamName ?
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(notNullValue, FakeParameterName) :
            () => Arguments.NotEmptyNorWhiteSpaceOnlyOrException(notNullValue);
    }
}
