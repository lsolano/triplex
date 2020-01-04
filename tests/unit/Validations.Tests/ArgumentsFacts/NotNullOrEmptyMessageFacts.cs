using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class NotNullOrEmptyMessageFacts
    {
        /* 
         * null paramName => throws ArgumentNullException with ParamName == 'paramName'
         * '' (empty) paramName => throws ArgumentOutOfRangeException with ParamName == 'paramName' and ActualValue == 0
         */

            [Test]
        public void With_Null_ParamName_Throws_ArgumentNullException() {
            Assert.That(() => Arguments.NotNullOrEmpty("dummyValue", null),
                Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
        }
    }
}
