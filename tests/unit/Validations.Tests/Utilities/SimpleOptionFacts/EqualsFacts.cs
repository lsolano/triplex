using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.SimpleOptionFacts;

[TestFixture]
internal sealed class EqualsFacts
{
    [Test]
    public void With_Null_Returns_False([Values] bool full)
    {
        SimpleOption<string> option = FullOrEmptyOption(full);

        Assert.That(option.Equals(null), Is.False);
    }

    [Test]
    public void With_Some_Empty_Returns_False([Values] bool firstFull, [Values] bool secondAsObject)
    {
        SimpleOption<string> first = FullOrEmptyOption(firstFull);
        SimpleOption<string> second = FullOrEmptyOption(!firstFull);

        AssertNotEquals(secondAsObject, first, second);
    }

    [Test]
    public void With_Both_Non_Empty_And_Different_Values_Returns_False([Values] bool secondAsObject)
    {
        SimpleOption<string> first = SimpleOption.SomeNotNull("Cat");
        SimpleOption<string> second = SimpleOption.SomeNotNull("Dog");

        AssertNotEquals(secondAsObject, first, second);
    }

    [Test]
    public void With_Both_Non_Empty_And_Same_Values_Returns_True([Values] bool secondAsObject)
    {
        SimpleOption<int> first = SimpleOption.SomeNotNull(1_024);
        SimpleOption<int> second = SimpleOption.SomeNotNull(1_024);

        AssertEquals(secondAsObject, first, second);
    }

    private static void AssertNotEquals<T>(bool secondAsObject, SimpleOption<T> first, SimpleOption<T> second)
        => AssertEquality(secondAsObject, first, second, false);

    private static void AssertEquals<T>(bool secondAsObject, SimpleOption<T> first, SimpleOption<T> second)
        => AssertEquality(secondAsObject, first, second, true);

    private static void AssertEquality<T>(bool secondAsObject, SimpleOption<T> first, SimpleOption<T> second,
        bool expected)
    {
        if (secondAsObject)
        {
            Assert.That(first.Equals(second), Is.EqualTo(expected));
        }
        else
        {
            Assert.That(first.Equals((object)second), Is.EqualTo(expected));
        }
    }

    private static SimpleOption<string> FullOrEmptyOption(bool full)
    {
        const string value = "Hello Options";

        return full ? SimpleOption.SomeNotNull(value) : SimpleOption.None<string>();
    }
}
