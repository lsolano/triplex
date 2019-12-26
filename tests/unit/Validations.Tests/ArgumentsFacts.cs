using System;
using NUnit.Framework;

namespace Triplex.Validations.Tests
{
    /// <summary>
    /// Messages variants are: DataOnly (monadic), DataAndName (diadic), DataNameAndCustomMessage (triadic).
    /// </summary>
#pragma warning disable CA1812 // Allow internal classes not used "anywhere" but by test framework.
    [TestFixture]
    public static class ArgumentsFacts
    {
        private const string DefaultPrefix = "Value cannot be null.";

        private const string ParamName = "username";
        private static readonly string CustomMessage = $"Look caller: '{ParamName}' can't be null.";

        private static string BuildFinalMessage(string customMessagePrefix, string paramName)
            => $"{customMessagePrefix}{Environment.NewLine}Parameter name: {paramName}";

        [TestFixture]
        internal sealed class NotNullDataOnlyMessageFacts
        {
            [Test]
            public void With_Null_Throws_ArgumentNullException() => Assert.That(() => Arguments.NotNull(null), Throws.ArgumentNullException);

            [Test]
            public void With_Peter_Throws_Nothing() => Assert.That(() => Arguments.NotNull("Peter"), Throws.Nothing);
        }

        [TestFixture]
        internal sealed class NotNullDataAndNameMessageFacts
        {
            [Test]
            public void With_Null_Throws_ArgumentNullException_Using_Param_Name()
            {
                string expectedMessage = BuildFinalMessage(DefaultPrefix, ParamName);
                Assert.That(() => Arguments.NotNull(null, ParamName),
                            Throws.ArgumentNullException.With.Message.EqualTo(expectedMessage));
            }

            [Test]
            public void With_Peter_Throws_Nothing() => Assert.That(() => Arguments.NotNull("Peter", ParamName), Throws.Nothing);
        }

        [TestFixture]
        internal sealed class NotNullDataNameAndCustomMessageMessageFacts
        {
            [Test]
            public void With_Null_Throws_ArgumentNullException_Using_Param_Name()
            {
                string expectedMessage = BuildFinalMessage(CustomMessage, ParamName);
                Assert.That(() => Arguments.NotNull(null, ParamName, CustomMessage),
                            Throws.ArgumentNullException.With.Message.EqualTo(expectedMessage));
            }

            [Test]
            public void With_Peter_Throws_Nothing() => Assert.That(() => Arguments.NotNull("Peter", ParamName, CustomMessage), Throws.Nothing);
        }
    }
#pragma warning restore CA1812 // Allow internal classes not used "anywhere" but by test framework.
}
