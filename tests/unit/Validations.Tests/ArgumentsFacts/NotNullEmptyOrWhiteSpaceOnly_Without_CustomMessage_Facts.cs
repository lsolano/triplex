using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class NotNullEmptyOrWhiteSpaceOnly_Without_CustomMessage_Facts
    {
        [Test]
        public void With_Null_Value_Throws_ArgumentNullException()
        {
            string? dummyParam = null;

            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam!, nameof(dummyParam)),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(nameof(dummyParam)));
        }

        [Test]
        public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
        {
            string? dummyParam = string.Empty;

            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam)),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(nameof(dummyParam)));
        }

        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        [TestCase("\n\r\t ")]
        public void With_Common_White_Space_Value_Throws_ArgumentFormatException(in string? dummyParam)
        {
            string? copy = dummyParam;
            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(copy, nameof(dummyParam)),
                Throws.InstanceOf<Exceptions.ArgumentFormatException>()
                .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(dummyParam)));
        }

        [TestCase("peter")]
        [TestCase("parker ")]
        [TestCase(" Peter Parker Is Spiderman ")]
        public void With_Valid_Values_Returns_Input_Value(in string? someValue)
        {
            string validatedValue = Arguments.NotNullEmptyOrWhiteSpaceOnly(someValue, nameof(someValue));

            Assert.That(validatedValue, Is.SameAs(someValue));
        }

        [Test]
        public void With_Null_ParamName_Throws_ArgumentNullException()
        {
            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly("dummyValue", null!),
                Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
        }

        [Test]
        public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
        {
            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly("dummyValue", string.Empty),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                .With.Property(nameof(ArgumentOutOfRangeException.ParamName)).EqualTo("paramName")
                .And.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(0));
        }

        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        [TestCase("\n\r\t ")]
        public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException(in string? paramName)
        {
            string? paramNameValue = paramName;
            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly("dummyValue", paramNameValue!),
                Throws.InstanceOf<Exceptions.ArgumentFormatException>()
                .With.Property(nameof(Exceptions.ArgumentFormatException.ParamName)).EqualTo("paramName"));
        }
    }
}
