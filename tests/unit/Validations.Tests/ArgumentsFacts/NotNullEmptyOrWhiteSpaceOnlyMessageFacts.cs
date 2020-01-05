using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class NotNullEmptyOrWhiteSpaceOnlyMessageFacts
    {
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("\r")]
        [TestCase("\t")]
        [TestCase("\n\r\t ")]
        public void With_Common_White_Space_Value_Throws_ArgumentFormatException(string dummyParam)
        {
            Assert.That(() => Arguments.NotNullEmptyOrWhiteSpaceOnly(dummyParam, nameof(dummyParam), "custom message"),
                Throws.InstanceOf<Exceptions.ArgumentFormatException>()
                .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(dummyParam)));
        }
    }
}
