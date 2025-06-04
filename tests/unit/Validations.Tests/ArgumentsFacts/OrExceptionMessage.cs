namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class OrExceptionMessage
{
    private const string FakeParameterName = "veryFakeParameterName";

    [Test]
    public void With_Null_Throws_ArgumentNullException_Using_Correct_Param_Name([Values] bool useCustomParamName)
    {
        string email = null!;

        Action act = useCustomParamName ?
            () => Arguments.OrException(email, FakeParameterName) :
            () => Arguments.OrException(email);

        ArgumentNullException? exception = Assert.Throws<ArgumentNullException>(() => act());

        string finalParamName = useCustomParamName ? FakeParameterName : nameof(email);
        (string messagePartOne, string messagePartTwo) = ExceptionMessagePartsFor(paramName: finalParamName);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Has.Property(nameof(ArgumentException.ParamName)).EqualTo(finalParamName));
            Assert.That(exception, Has.Property(nameof(ArgumentException.Message)).Contains(messagePartOne));
            Assert.That(exception, Has.Property(nameof(ArgumentException.Message)).Contains(messagePartTwo));
        }
    }

    [Test]
    public void With_Null_Throws_ArgumentNullException_Using_Correct_Message([Values] bool useCustomParamName)
    {
        object user = null!;
        string finalParamName = useCustomParamName ? FakeParameterName : nameof(user);

        Action act = useCustomParamName ?
            () => Arguments.OrException(user, FakeParameterName) :
            () => Arguments.OrException(user);

        ArgumentNullException? exception = Assert.Throws<ArgumentNullException>(() => act());

        (string messagePartOne, string messagePartTwo) = ExceptionMessagePartsFor(paramName: finalParamName);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Has.Property(nameof(ArgumentException.Message)).Contains(messagePartOne));
            Assert.That(exception, Has.Property(nameof(ArgumentException.Message)).Contains(messagePartTwo));
        }
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
        List<int> fibonacci = [1, 1, 2, 3, 5];

        Func<IList<int>> listCheck = BuildCheckForNotNull(fibonacci, useCustomParamName);

        Assert.That(listCheck(), Is.SameAs(fibonacci));
    }
    
    private static Func<T> BuildCheckForNotNull<T>(T notNullValue, bool useCustomParamName) where T : class
    {
        return useCustomParamName ?
            () => Arguments.OrException(notNullValue, FakeParameterName) :
            () => Arguments.OrException(notNullValue);
    }

    private static (string first, string second) ExceptionMessagePartsFor(string paramName)
        => ("Value cannot be null.", "Parameter '{paramName}'".Replace("{paramName}", paramName));
}
