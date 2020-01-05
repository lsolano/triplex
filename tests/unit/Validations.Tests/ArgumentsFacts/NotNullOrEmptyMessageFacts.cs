using NUnit.Framework;
using System;

namespace Triplex.Validations.Tests.ArgumentsFacts
{
    [TestFixture]
    internal sealed class NotNullOrEmptyMessageFacts
    {
        /* 
         *      null paramName => throws ArgumentNullException with ParamName == 'paramName'
         *      '' (empty) paramName => throws ArgumentOutOfRangeException with ParamName == 'paramName' and ActualValue == 0
         * 
         *      null value => throws ArgumentNullException with ParamName == value of 'paramName'
         *      '' (empty) value => throws ArgumentOutOfRangeException with ParamName == value of 'paramName' and ActualValue == 0
         *      ' ', \t, \r, \n (common white-space chars) value => throws ArgumentFormatException with ParamName == value of 'paramName'
         */

        [Test]
        public void With_Null_ParamName_Throws_ArgumentNullException()
        {
            Assert.That(() => Arguments.NotNullOrEmpty("dummyValue", null),
                Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("paramName"));
        }

        [Test]
        public void With_Empty_ParamName_Throws_ArgumentOutOfRangeException()
        {
            Assert.That(() => Arguments.NotNullOrEmpty("dummyValue", string.Empty),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                .With.Property(nameof(ArgumentOutOfRangeException.ParamName)).EqualTo("paramName")
                .And.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(0));
        }

        [Test]
        public void With_Null_Value_Throws_ArgumentNullException()
        {
            string dummyParam = null;
            Assert.That(() => Arguments.NotNullOrEmpty(dummyParam, nameof(dummyParam)),
                Throws.ArgumentNullException
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(nameof(dummyParam)));
        }

        //ArgumentOutOfRangeException
        [Test]
        public void With_Empty_Value_Throws_ArgumentOutOfRangeException()
        {
            string dummyParam = string.Empty;
            Assert.That(() => Arguments.NotNullOrEmpty(dummyParam, nameof(dummyParam)),
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo(nameof(dummyParam)));
        }


        //[TestCase(" ")]
        //[TestCase("\n")]
        //[TestCase("\r")]
        //[TestCase("\t")]
        //[TestCase("\n\r\t ")]
        //public void With_Common_White_Space_Value_Throws_ArgumentFormatException(string dummyParam)
        //{
        //    Assert.That(() => Arguments.NotNullOrEmpty(dummyParam, nameof(dummyParam)),
        //        Throws.InstanceOf<Exceptions.ArgumentFormatException>()
        //        .With.Property(nameof(ArgumentException.ParamName)).EqualTo(nameof(dummyParam)));
        //}
    }
}
