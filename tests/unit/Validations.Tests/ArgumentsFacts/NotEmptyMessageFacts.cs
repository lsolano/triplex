using Triplex.Validations.Exceptions;

namespace Triplex.Validations.Tests.ArgumentsFacts;

internal static class NotEmptyMessageFacts
{
    private const string CustomMessage = "Can't be null or empty from test case";

    [TestFixture]
    internal sealed class WithInvalidCustomMessage
    {
        [Test]
        public void With_Null_Throws_ArgumentNullException()
        {
            string someInput = "dummyValue";
            Assert.That(() => Arguments.NotEmptyOrExceptionWithMessage(someInput, null!, nameof(someInput)),
                Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
        }

        [Test]
        public void With_Empty_Throws_ArgumentOutOfRangeException()
        {
            string someInput = "dummyValue";
            Assert.That(() => Arguments.NotEmptyOrExceptionWithMessage(someInput, string.Empty, nameof(someInput)),
                Throws.InstanceOf<ArgumentFormatException>()
                .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("customMessage"));
        }
    }

    internal sealed class WithInvalidParamName : BaseFixtureForOptionalCustomMessage
    {
        public WithInvalidParamName(bool useCustomErrorMessage) : base(useCustomErrorMessage)
        {
        }

        [Test]
        public void With_Null_ParamName_Throws_ArgumentNullException()
        {
            Assert.That(() => NotNullOrEmpty("dummyValue", null, CustomMessage, UseCustomErrorMessage),
                Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
        }

        [Test]
        public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
        {
            Assert.That(() => NotNullOrEmpty("dummyValue", string.Empty, CustomMessage, UseCustomErrorMessage),
                Throws.InstanceOf<ArgumentFormatException>()
                .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo("paramName"));
        }
    }

    internal sealed class WithInvalidValueMessage : BaseFixtureForOptionalCustomMessage
    {
        public WithInvalidValueMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
        {
        }

        [Test]
        public void With_Null_Value_Throws_ArgumentNullException()
        {
            string? dummyParam = null;

            Constraint exceptionConstraint = AddMessageConstraint(
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                                                 .EqualTo(nameof(dummyParam)),
                UseCustomErrorMessage);

            Assert.That(() => NotNullOrEmpty(dummyParam, nameof(dummyParam), CustomMessage, UseCustomErrorMessage),
                exceptionConstraint);
        }

        [Test]
        public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
        {
            string? dummyParam = string.Empty;

            Constraint exceptionConstraint = AddMessageConstraint(
                Throws.InstanceOf<ArgumentFormatException>()
                .With.Property(nameof(ArgumentFormatException.ParamName)).EqualTo(nameof(dummyParam)),
                UseCustomErrorMessage);

            Assert.That(() => NotNullOrEmpty(dummyParam, nameof(dummyParam), CustomMessage, UseCustomErrorMessage),
                exceptionConstraint);
        }

        private static Constraint AddMessageConstraint(Constraint messageConstraint, bool useCustomErrorMessage)
            => useCustomErrorMessage ? messageConstraint.With.Message.StartsWith(CustomMessage) : messageConstraint;
    }

    internal sealed class WithValidValueMessage : BaseFixtureForOptionalCustomMessage
    {
        public WithValidValueMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
        {
        }

        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        [TestCase("\n\r\t ")]
        public void With_Common_White_Space_Sequences_Value_Throws_Nothing(string? dummyParam)
        {
            string myDummyValue = NotNullOrEmpty(dummyParam, nameof(dummyParam), CustomMessage, UseCustomErrorMessage);

            Assert.That(myDummyValue, Is.SameAs(dummyParam));
        }

        [TestCase("peter")]
        [TestCase("parker ")]
        [TestCase(" Peter Parker Is Spiderman ")]
        public void With_Non_Empty_Values_Throws_Nothing(string? someParam)
        {
            string myDummyValue = NotNullOrEmpty(someParam, nameof(someParam), CustomMessage, UseCustomErrorMessage);

            Assert.That(myDummyValue, Is.SameAs(someParam));
        }
    }

    private static string NotNullOrEmpty(
        string? value,
        string? paramName,
        string? customMessage,
        bool useCustomErrorMessage)
        => useCustomErrorMessage ? Arguments.NotEmptyOrExceptionWithMessage(value, customMessage!, paramName!)
                                 : Arguments.NotEmptyOrException(value, paramName!);
}
