namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture]
internal sealed class BetweenOrExceptionMessage
{
    [TestCase(1, 2)]
    [TestCase(0, 1)]
    public void Value_Can_Be_Equals_To_From_And_To(int from, int to)
    {
        const int val = 1;

        int actual = Arguments.BetweenOrException(val, from, to);

        Assert.That(actual, Is.EqualTo(val));
    }

    [TestCase(1, 0)]
    [TestCase(5, 5)]
    public void With_Inverted_And_Closed_Range_Throws_ArgumentOutOfRangeException(int from, int to)
        => Assert.That(() => Arguments.BetweenOrException(1, from, to),
            Throws.InstanceOf<ArgumentOutOfRangeException>());

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException([Values] bool useCustomParamName)
    {
        const string FakeParameterName = "fakeCodeParam";
        const string code = null!;

        Action act = useCustomParamName ?
            () => Arguments.BetweenOrException(code, "000", "002", FakeParameterName) :
            () => Arguments.BetweenOrException(code, "000", "002");

        string finalParamName = useCustomParamName ? FakeParameterName : nameof(code);

        Assert.That(() => act(), Throws.ArgumentNullException
            .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(finalParamName));
    }

    [Test]
    public void With_Null_From_Throws_ArgumentNullException()
        => Assert.That(() => Arguments.BetweenOrException("b", null!, "c"), Throws.ArgumentNullException);

    [Test]
    public void With_Null_To_Throws_ArgumentNullException()
        => Assert.That(() => Arguments.BetweenOrException("b", "a", null!), Throws.ArgumentNullException);
}
