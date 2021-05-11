using System;

using NUnit.Framework;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    internal sealed class NotEmptyGuidMessage : BaseFixtureForOptionalCustomMessage
    {
        private const string CustomMessage = "The Id can not be an empty Guid.";
        private const string DefaultParameterName = "Id";

        public NotEmptyGuidMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
        {
        }

        [Test]
        public void With_New_Guid_Throws_Nothing()
            => Assert.That(() => NotEmpty(Guid.NewGuid(), DefaultParameterName, CustomMessage, UseCustomErrorMessage), Throws.Nothing);

        [Test]
        public void Returns_Value_When_No_Exception()
        {
            Guid rawValue = Guid.NewGuid();
            Guid value = NotEmpty(rawValue, nameof(rawValue), CustomMessage, UseCustomErrorMessage);

            Assert.That(rawValue, Is.EqualTo(value));
        }

        [Test]
        public void With_Empty_Throws_ArgumentException()
            => Assert.That(() => NotEmpty(Guid.Empty, DefaultParameterName, CustomMessage, UseCustomErrorMessage),
                Throws.ArgumentException);

        [Test]
        public void With_Null_ParamName_Throws_ArgumentNullException()
            => Assert.That(() => NotEmpty(Guid.Empty, null, CustomMessage, UseCustomErrorMessage),
                Throws.ArgumentNullException.With.Message.Contains("paramName")
                .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));

        [Test]
        public void With_Null_CustomMessage_Throws_ArgumentNullException()
        {
            Assume.That(UseCustomErrorMessage, Is.True);

            Assert.That(() => NotEmpty(Guid.Empty, DefaultParameterName, null, UseCustomErrorMessage),
                Throws.ArgumentNullException.With.Message.Contains("customMessage")
                .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
        }

        private static Guid NotEmpty(in Guid value, in string paramName, in string customMessage, bool useCustomMessage)
            => useCustomMessage?
                Arguments.NotEmpty(value, paramName, customMessage)
                :Arguments.NotEmpty(value, paramName);
    }
}