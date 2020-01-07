using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    internal sealed class ValidEnumerationMemberMessage : BaseFixtureForOptionalCustomMessage
    {
        public enum Color { Black = 0, White = 1 }

        public ValidEnumerationMemberMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
        {
        }

        [TestCase(Color.Black)]
        [TestCase(Color.White)]
        public void With_Valid_Constants_Throws_Nothing(Color someColor)
        {
            Assert.That(() => ValidEnumerationMember(someColor, nameof(someColor), null, UseCustomErrorMessage),
                Throws.Nothing);
        }

        [Test]
        public void With_Invalid_Constants_Throws_ArgumentOutOfRangeException(
            [Values(-1, 2)] Color someColor,
            [Values("this is not OK", "Invalid value")] string customError)
        {
            string expectedMessage = BuildExpectedMessageForValidEnumerationMember(someColor, nameof(Color),
                nameof(someColor), customError, UseCustomErrorMessage);

            Assert.That(() => ValidEnumerationMember(someColor, nameof(someColor), customError, UseCustomErrorMessage),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Message.EqualTo(expectedMessage));
        }

        [TestCase(Color.Black)]
        [TestCase(Color.White)]
        public void With_Valid_Constants_Returns_Value(in Color someColor)
        {
            Color validatedColor =
                ValidEnumerationMember(someColor, nameof(someColor), "some custom error msg", UseCustomErrorMessage);

            Assert.That(validatedColor, Is.EqualTo(someColor));
        }

#if NETFRAMEWORK
            private static readonly string DefaultErrorTemplate = $"Value is not a member of enum {{typeName}}.{System.Environment.NewLine}Parameter name: {{paramName}}{System.Environment.NewLine}Actual value was {{actualValue}}.";
            private static readonly string CustomErrorTemplate = $"{{prefix}}{System.Environment.NewLine}Parameter name: {{paramName}}{System.Environment.NewLine}Actual value was {{actualValue}}.";
#endif
#if NETCOREAPP
        private static readonly string DefaultErrorTemplate = $"Value is not a member of enum {{typeName}}. (Parameter '{{paramName}}'){Environment.NewLine}Actual value was {{actualValue}}.";
        private static readonly string CustomErrorTemplate = $"{{prefix}} (Parameter '{{paramName}}'){Environment.NewLine}Actual value was {{actualValue}}.";
#endif
        private static string BuildExpectedMessageForValidEnumerationMember(in object value, in string typeName, in string paramName, in string customErrorMessage, in bool useCustomErrorMessage)
        {
            string baseMessage = useCustomErrorMessage
                ? CustomErrorTemplate.Replace("{prefix}", customErrorMessage)
                : DefaultErrorTemplate.Replace("{typeName}", typeName);

            return baseMessage.Replace("{paramName}", paramName)
                              .Replace("{actualValue}", value.ToString());
        }

        private static TValue ValidEnumerationMember<TValue>(in TValue value, in string paramName, in string customMessage,
            in bool useCustomMessage) where TValue : Enum
        {
            return useCustomMessage ? Arguments.ValidEnumerationMember(value, paramName, customMessage) : Arguments.ValidEnumerationMember(value, paramName);
        }
    }
}
