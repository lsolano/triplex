using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    internal sealed class NotNullMessage : BaseFixtureForOptionalCustomMessage
    {
        private const string CustomMessage = "Look caller: the input can't be null.";
        private const string DefaultParameterName = "username";

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
        public void Returns_Value_When_No_Exception_For_Strings(in string value)
        {
            string name = NotNull(value, nameof(value), CustomMessage, UseCustomErrorMessage);

            Assert.That(name, Is.EqualTo(value));
        }

        [Test]
        public void Returns_Value_When_No_Exception_For_List()
        {
            var fibonacci = new List<int> { 1, 1, 2, 3, 5 };
            IList<int> myFibonacci = NotNull(fibonacci, nameof(fibonacci), CustomMessage, UseCustomErrorMessage);

            CollectionAssert.AreEqual(fibonacci, myFibonacci);
        }

#if NETFRAMEWORK
            private static readonly string DefaultErrorTemplate = $"Value cannot be null.{System.Environment.NewLine}Parameter name: {{paramName}}";
            private static string BuildFinalMessage(in string customMessagePrefix, in string paramName, in bool useCustomErrorMessage)
                => useCustomErrorMessage ? $"{customMessagePrefix}{System.Environment.NewLine}Parameter name: {paramName}"
                    : DefaultErrorTemplate.Replace("{paramName}", paramName);
#endif
#if NETCOREAPP
        private const string DefaultErrorTemplate = "Value cannot be null. (Parameter '{paramName}')";
        private static string BuildFinalMessage(in string customMessagePrefix, in string paramName, in bool useCustomErrorMessage)
            => useCustomErrorMessage ? $"{customMessagePrefix} (Parameter '{paramName}')"
                : DefaultErrorTemplate.Replace("{paramName}", paramName);
#endif

        private static TValue NotNull<TValue>(in TValue value, in string paramName, in string customMessage,
            bool useCustomMessage)
            where TValue : class => useCustomMessage
            ? Arguments.NotNull(value, paramName, customMessage)
            : Arguments.NotNull(value, paramName);
    }
}
