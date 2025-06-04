namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class MemberOfOrExceptionWithMessageMessage
{
    private const string CustomMessage = "some custom error msg";
    private const string CustomParamName = "fakeParamName";

    [Test]
    public void With_Invalid_Constants_Throws_ArgumentOutOfRangeException(
        [Values(-1, 2)] Color someColor,
        [Values] bool useCustomParamName)
    {
        string finalParamName = useCustomParamName ? CustomParamName : nameof(someColor);

        Action act = useCustomParamName ?
            () => Arguments.MemberOfOrExceptionWithMessage(someColor, CustomMessage, finalParamName) :
            () => Arguments.MemberOfOrExceptionWithMessage(someColor, CustomMessage);

        IEnumerable<string> exceptionParts = ExceptionMessagePartsFor(someColor, finalParamName);
 
        Assert.That(() => act(),
            Throws.InstanceOf<ArgumentOutOfRangeException>()
                  .WithMessageContainsAll(exceptionParts));
    }

    [Test]
    public void With_Valid_Color_Constants_Returns_Value([Values] Color someColor, [Values] bool useCustomParamName)
    {
        Color validatedColor = useCustomParamName ? 
            Arguments.MemberOfOrExceptionWithMessage(someColor, CustomMessage, CustomParamName):
            Arguments.MemberOfOrExceptionWithMessage(someColor, CustomMessage);

        Assert.That(validatedColor, Is.EqualTo(someColor));
    }

    private static IEnumerable<string> ExceptionMessagePartsFor(object value, string paramName)
    {
        yield return CustomMessage;
        yield return "Parameter";
        yield return paramName;
        yield return "Actual value was {actualValue}.".Replace("{actualValue}", value.ToString());
    }
}
