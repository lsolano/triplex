using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    internal sealed class ValidBase64MessageFacts : BaseFixtureForOptionalCustomMessage
    {
        private const string CustomMessage = "Look caller: the input can't be null.";
        private const string DefaultParameterName = "username";

        public ValidBase64MessageFacts(bool useCustomErrorMessage) : base(useCustomErrorMessage)
        {
        }

        [Test]
        public void With_Null_Throws_ArgumentNullException()
        {
            string expectedMessage = BuildFinalMessage(CustomMessage, DefaultParameterName, UseCustomErrorMessage);

            Assert.That(() => ValidBase64((string?)null, DefaultParameterName, CustomMessage, UseCustomErrorMessage),
                        Throws.ArgumentNullException.With.Message.EqualTo(expectedMessage)
                            .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo(DefaultParameterName));
        }

        [TestCase("c3BpZGVyIG1hbgá=")]
        [TestCase("cGV0ZXí")]
        [TestCase("c3BpZGVyIG1hbg=")]
        public void With_Invalid_Value_Throws_FormatException(string? iAmInvalid)
            => Assert.That(() => ValidBase64(iAmInvalid!, nameof(iAmInvalid), CustomMessage, UseCustomErrorMessage),
                Throws.InstanceOf<FormatException>());

        [TestCase("cGV0ZXI=")]
        [TestCase("c3BpZGVyIG1hbg==")]
        public void Returns_Value_When_No_Exception(in string? value)
        {
            string validatedValue = ValidBase64(value, nameof(value), CustomMessage, UseCustomErrorMessage);

            Assert.That(validatedValue, Is.EqualTo(value));
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

        private static string ValidBase64(in string? value, in string? paramName, in string? customMessage,
            bool useCustomMessage)
            => useCustomMessage
                ? Arguments.ValidBase64(value, paramName!, customMessage!)
                : Arguments.ValidBase64(value, paramName!);
    }
}
