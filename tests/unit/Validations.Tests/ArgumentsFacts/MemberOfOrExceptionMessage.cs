namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class MemberOfOrExceptionMessage
{
    private const string FakeParamName = "someOtherColor";

    [Test]
    public void With_Invalid_Constants_And_Custom_Param_Name_Throws_ArgumentOutOfRangeException(
        [Values(-1, 2)] Color someColor, [Values] bool useCustomParamName)
    {
        string finalParamName = useCustomParamName ? FakeParamName : nameof(someColor);

        Action act = useCustomParamName ?
            () => Arguments.MemberOfOrException(someColor, finalParamName) :
            () => Arguments.MemberOfOrException(someColor);

        IEnumerable<string> exceptionParts = ExceptionMessagePartsFor(someColor, finalParamName, nameof(Color));

        Assert.That(() => act(), Throws.InstanceOf<ArgumentOutOfRangeException>()
            .WithMessageContainsAll(exceptionParts));
    }

    [Test]
    public void With_Valid_Color_Constants_Returns_Value([Values] Color someColor, [Values] bool useCustomParamName)
    {
        Color validatedColor = useCustomParamName ?
            Arguments.MemberOfOrException(someColor, FakeParamName)
            : Arguments.MemberOfOrException(someColor);

        Assert.That(validatedColor, Is.EqualTo(someColor));
    }

    private static IEnumerable<string> ExceptionMessagePartsFor(object value, string paramName, string typeName)
    {
        yield return "Value is not a member of enum {typeName}.".Replace("{typeName}", typeName);
        yield return "Parameter '{paramName}'".Replace("{paramName}", paramName);
        yield return "Actual value was {actualValue}.".Replace("{actualValue}", value.ToString());
    }
}
