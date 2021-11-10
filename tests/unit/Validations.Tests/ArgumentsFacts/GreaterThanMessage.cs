namespace Triplex.Validations.Tests.ArgumentsFacts;

internal sealed class GreaterThanMessage : BaseFixtureForOptionalCustomMessage
{
    private const string CustomError = "Must be less than or equal other.";

    public GreaterThanMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
    {
    }

    [TestCase(-1, -2)]
    [TestCase(0, -1)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(2, -1)]
    public void With_Valid_Integers_Throws_Nothing(in int theValue, in int other)
    {
        int validatedValue = Arguments.GreaterThan(theValue, other, nameof(theValue), CustomError);

        Assert.That(validatedValue, Is.EqualTo(theValue));
    }

    [TestCase(-1L, -2L)]
    [TestCase(0L, -1L)]
    [TestCase(1L, 0L)]
    [TestCase(2L, 1L)]
    [TestCase(2L, -1L)]
    public void With_Valid_Longs_Throws_Nothing(in long theValue, in long other)
    {
        long validatedValue = GreaterThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

        Assert.That(validatedValue, Is.EqualTo(theValue));
    }

    [TestCase("b", "a")]
    [TestCase("c", "b")]
    public void With_Valid_Strings_Throws_Nothing(in string theValue, in string other)
    {
        string validatedValue = GreaterThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

        Assert.That(validatedValue, Is.EqualTo(theValue));
    }

    [TestCase(-2, -1)]
    [TestCase(-1, 0)]
    [TestCase(-2, 2)]
    [TestCase(-2, -2)]
    [TestCase(2, 2)]
    public void With_Less_Or_Equal_Integers_Throws_ArgumentOutOfRangeException(in int theValue, in int other)
    {
        Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
        (int theValueCopy, int otherCopy) = (theValue, other);

        Assert.That(() => GreaterThan(theValueCopy, otherCopy, nameof(theValue), CustomError, UseCustomErrorMessage),
            throwsOutOfRange);
    }

    [TestCase("a", "b")]
    [TestCase("b", "c")]
    [TestCase("a", "c")]
    [TestCase("a", "a")]
    [TestCase("c", "c")]
    public void With_Less_Or_Equal_Strings_Throws_ArgumentOutOfRangeException(in string theValue, in string other)
    {
        Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
        (string theValueCopy, string otherCopy) = (theValue, other);

        Assert.That(() => GreaterThan(theValueCopy, otherCopy, nameof(theValue), CustomError, UseCustomErrorMessage),
            throwsOutOfRange);
    }

    private Constraint BuildConstraint(in object theValue, in object other, in string paramName, in string customError)
    {
        Constraint throwsOutOfRange = Throws.InstanceOf<ArgumentOutOfRangeException>()
            .With.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(theValue)
            .With.Property(nameof(ArgumentException.ParamName)).EqualTo(paramName);

        return UseCustomErrorMessage
            ? throwsOutOfRange.With.Message.StartWith(customError)
            : (Constraint)throwsOutOfRange.With.Message.Contains(other.ToString());
    }

    [Test]
    public void With_Null_Value_Throws_ArgumentNullException()
    {
        const string? someString = null;

        Assert.That(() => GreaterThan(someString, "abc", nameof(someString), CustomError, UseCustomErrorMessage),
            Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("value"));
    }

    [Test]
    public void With_Null_Other_Throws_ArgumentNullException()
    {
        const string? someString = "abc";

        Assert.That(() => GreaterThan(someString, null, nameof(someString), CustomError, UseCustomErrorMessage),
            Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("other"));
    }

    [Test]
    public void With_Null_CustomMessage_Throws_ArgumentNullException()
    {
        Assume.That(UseCustomErrorMessage, Is.True);

        const string? someString = "abc";

        Assert.That(() => GreaterThan(someString, "xyz", nameof(someString), null, UseCustomErrorMessage),
            Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
    }

    private static TComparable GreaterThan<TComparable>(
        in TComparable? value,
        in TComparable? other,
        in string? paramName,
        in string? customError,
        in bool useCustomErrorMessage) where TComparable : IComparable<TComparable>
    {
        return useCustomErrorMessage
            ? Arguments.GreaterThan(value, other, paramName!, customError!)
            : Arguments.GreaterThan(value, other, paramName!);
    }
}
