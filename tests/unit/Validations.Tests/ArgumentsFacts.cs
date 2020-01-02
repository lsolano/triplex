using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Triplex.Validations.Tests
{
    /// <summary>
    /// Messages variants are: DataOnly (monadic), DataAndName (dyadic), DataNameAndCustomMessage (triadic).
    /// </summary>
#pragma warning disable CA1812 // Allow internal classes not used "anywhere" but by test framework.
    [TestFixture]
    public static class ArgumentsFacts
    {
        private const string DefaultParameterName = "username";
        private const string CustomMessage = "Look caller: the input can't be null.";

        [TestFixture(false)]
        [TestFixture(true)]
        internal abstract class BaseFixture
        {
            protected readonly bool UseCustomErrorMessage;

            protected BaseFixture(bool useCustomErrorMessage) => UseCustomErrorMessage = useCustomErrorMessage;
        }

        internal sealed class NotNullMessage : BaseFixture
        {
            public NotNullMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [Test]
            public void With_Null_Throws_ArgumentNullException(
                [Values("username", "someInput", "iAmAParameter")] string paramName,
                [Values("not good", "check your input")] string customErrorMessage)
            {
                string expectedMessage = BuildFinalMessage(customErrorMessage, paramName, UseCustomErrorMessage);

                Assert.That(() => NotNull((string)null, paramName, customErrorMessage, UseCustomErrorMessage),
                            Throws.ArgumentNullException.With.Message.EqualTo(expectedMessage)
                                .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo(paramName));
            }

            [Test]
            public void With_Peter_Throws_Nothing()
                => Assert.That(() => NotNull("Peter", DefaultParameterName, CustomMessage, UseCustomErrorMessage), Throws.Nothing);

            [TestCase("")]
            [TestCase("peter")]
            [TestCase("spider-man")]
            public void Returns_Value_When_No_Exception_For_Strings(string value)
            {
                string name = NotNull(value, nameof(value), CustomMessage, UseCustomErrorMessage);

                Assert.That(name, Is.EqualTo(value));
            }

            [Test]
            public void Returns_Value_When_No_Exception_For_List()
            {
                var fibonacci = new List<int> {1, 1, 2, 3, 5};
                IList<int> myFibonacci = NotNull(fibonacci, nameof(fibonacci), CustomMessage, UseCustomErrorMessage);

                CollectionAssert.AreEqual(fibonacci, myFibonacci);
            }

#if NETFRAMEWORK
            private static readonly string DefaultErrorTemplate = $"Value cannot be null.{System.Environment.NewLine}Parameter name: {{paramName}}";
            private static string BuildFinalMessage(string customMessagePrefix, string paramName, bool useCustomErrorMessage)
                => useCustomErrorMessage ? $"{customMessagePrefix}{System.Environment.NewLine}Parameter name: {paramName}"
                    : DefaultErrorTemplate.Replace("{paramName}", paramName);
#endif
#if NETCOREAPP
            private const string DefaultErrorTemplate = "Value cannot be null. (Parameter '{paramName}')";
            private static string BuildFinalMessage(string customMessagePrefix, string paramName, bool useCustomErrorMessage)
                => useCustomErrorMessage ? $"{customMessagePrefix} (Parameter '{paramName}')"
                    : DefaultErrorTemplate.Replace("{paramName}", paramName);
#endif
            
            private static TValue NotNull<TValue>(TValue value, string paramName, string customMessage,
                bool useCustomMessage)
                where TValue : class => useCustomMessage
                ? Arguments.NotNull(value, paramName, customMessage)
                : Arguments.NotNull(value, paramName);
        }

        internal sealed class ValidEnumerationMemberMessage : BaseFixture
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
            public void With_Valid_Constants_Returns_Value(Color someColor)
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
            private static string BuildExpectedMessageForValidEnumerationMember(object value, string typeName, string paramName, string customErrorMessage, bool useCustomErrorMessage)
            {
                string baseMessage = useCustomErrorMessage
                    ? CustomErrorTemplate.Replace("{prefix}", customErrorMessage)
                    : DefaultErrorTemplate.Replace("{typeName}", typeName);

                return baseMessage.Replace("{paramName}", paramName)
                                  .Replace("{actualValue}", value.ToString());
            }

            private static TValue ValidEnumerationMember<TValue>(TValue value, string paramName, string customMessage,
                bool useCustomMessage) where TValue : Enum
            {
                return useCustomMessage ? Arguments.ValidEnumerationMember(value, paramName, customMessage) : Arguments.ValidEnumerationMember(value, paramName);
            }
        }
    }
#pragma warning restore CA1812 // Allow internal classes not used "anywhere" but by test framework.
}
