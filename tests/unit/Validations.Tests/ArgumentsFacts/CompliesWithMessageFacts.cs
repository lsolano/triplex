using System;
using NUnit.Framework;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class CompliesWithMessageFacts
    {
        private const string PreconditionDescription = "Must be true.";

        [Test]
        public void With_True_Throws_Nothing() {
            Assert.That(() => Arguments.CompliesWith(2 + 2 == 4, "someParam", PreconditionDescription), Throws.Nothing);
        }

        [Test]
        public void With_False_Throws_ArgumentException() {
            const string paramName = "someParameter";
            Assert.That(() => Arguments.CompliesWith(2 + 2 == 5, paramName, PreconditionDescription),
                Throws.ArgumentException
                      .With.Property(nameof(ArgumentException.ParamName)).EqualTo(paramName)
                      .And.Message.Contains(PreconditionDescription));
        }

        [Test]
        public void With_Invalid_ParamName_Throws_ArgumentException([Values(null, "", " ", "\n\r\t ")] string paramName, [Values] bool precondition) {
            Assert.That(() => Arguments.CompliesWith(precondition, paramName, PreconditionDescription),
                Throws.InstanceOf<ArgumentException>()
                      .With.Property(nameof(ArgumentException.ParamName)).EqualTo("paramName"));
        }

        [Test]
        public void With_Invalid_PreconditionDescription_Throws_ArgumentException([Values(null, "", " ", "\n\r\t ")] string preconditionDescription, [Values] bool precondition) {
            Assert.That(() => Arguments.CompliesWith(precondition, "someParamName", preconditionDescription),
                Throws.InstanceOf<ArgumentException>()
                      .With.Property(nameof(ArgumentException.ParamName)).EqualTo("preconditionDescription"));
        }
    }
}