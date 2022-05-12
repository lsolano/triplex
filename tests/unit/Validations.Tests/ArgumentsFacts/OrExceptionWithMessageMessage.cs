namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class OrExceptionWithMessageMessage
{
    private const string CustomMessage = "Bad programmer, do not use NULLs.";
    private const string FakeParameterName = "veryFakeParameterName2";

    [Test]
    public void With_Null_Throws_ArgumentNullException_Using_Correct_Param_Name([Values] bool useCustomParamName)
    {
        string email = null!;

        string finalParamName = useCustomParamName ? FakeParameterName : nameof(email);

        Action act = useCustomParamName ?
            () => Arguments.OrExceptionWithMessage(email, CustomMessage, finalParamName) :
            () => Arguments.OrExceptionWithMessage(email, CustomMessage);

        Assert.That(() => act(),
            Throws.ArgumentNullException.With
                .Property(nameof(ArgumentNullException.ParamName)).EqualTo(finalParamName));
    }

    [Test]
    public void With_Null_Throws_ArgumentNullException_Using_Correct_Message([Values] bool useCustomParamName)
    {
        object user = null!;
        string finalParamName = useCustomParamName ? FakeParameterName : nameof(user);

        Action act = useCustomParamName ?
            () => Arguments.OrExceptionWithMessage(user, CustomMessage, finalParamName) :
            () => Arguments.OrExceptionWithMessage(user, CustomMessage);

        IEnumerable<string> exceptionParts = ExceptionMessagePartsFor(paramName: finalParamName);

        Assert.That(() => act(), Throws.ArgumentNullException.WithMessageContainsAll(exceptionParts));
    }

    [Test]
    public void With_Peter_Throws_Nothing([Values] bool useCustomParamName)
    {
        Func<string> act = BuildCheckForNotNull("Peter", useCustomParamName);

        Assert.That(() => _ = act(), Throws.Nothing);
    }

    [Test]
    public void Returns_Value_When_No_Exception_For_Strings([Values("", "peter", "spider-man")] string value,
        [Values] bool useCustomParamName)
    {
        Func<string> nameCheck = BuildCheckForNotNull(value, useCustomParamName);

        Assert.That(nameCheck(), Is.SameAs(value));
    }

    [Test]
    public void Returns_Value_When_No_Exception_For_List([Values] bool useCustomParamName)
    {
        List<int> fibonacci = new() { 1, 1, 2, 3, 5 };

        Func<IList<int>> listCheck = BuildCheckForNotNull(fibonacci, useCustomParamName);

        Assert.That(listCheck(), Is.SameAs(fibonacci));
    }

    private static Func<T> BuildCheckForNotNull<T>(T notNullValue, bool useCustomParamName) where T : class
    {
        return useCustomParamName ?
            () => Arguments.OrExceptionWithMessage(notNullValue, CustomMessage, FakeParameterName) :
            () => Arguments.OrExceptionWithMessage(notNullValue, CustomMessage);
    }

    private static IEnumerable<string> ExceptionMessagePartsFor(string paramName)
    {
        yield return CustomMessage;
        yield return "Parameter '{paramName}'".Replace("{paramName}", paramName);
    }
}
