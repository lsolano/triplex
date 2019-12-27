using System;
using NUnit.Framework;
using Triplex.ProtoDomainPrimitives.Numerics;

namespace Triplex.ProtoDomainPrimitives.Tests.Numerics
{
    internal static class PositiveIntegerFacts
    {
        private const int DefaultRawValue = 1024;
        private const string CustomErrorMessage = "Some dummy error message.";

        [TestFixture]
        internal sealed class ConstructorMessage
        {
            [Test]
            public void Rejects_Negatives_And_Zero([Values(int.MinValue, -1, 0)] int rawValue)
                => Assert.That(() => new PositiveInteger(rawValue),
                               Throws.InstanceOf<ArgumentOutOfRangeException>()
                                     .With
                                     .Message
                                     .StartsWith(PositiveInteger.DefaultErrorMessage));
            [Test]
            public void Rejects_Negatives_And_Zero_With_Custom_Message([Values(int.MinValue, -1, 0)] int rawValue)
                => Assert.That(() => new PositiveInteger(rawValue, CustomErrorMessage),
                    Throws.InstanceOf<ArgumentOutOfRangeException>()
                        .With
                        .Message
                        .StartsWith(CustomErrorMessage));

            [Test]
            public void Accepts_Positives([Values(1, DefaultRawValue, int.MaxValue)] int rawValue)
                => Assert.That(() => new PositiveInteger(rawValue), Throws.Nothing);

            [Test]
            public void Accepts_Positives_With_Custom_Message([Values(1, DefaultRawValue, int.MaxValue)] int rawValue)
                => Assert.That(() => new PositiveInteger(rawValue, CustomErrorMessage), Throws.Nothing);
        }

        [TestFixture]
        internal sealed class ValueProperty
        {
            [Test]
            public void Returns_Constructor_Provided_Value()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.Value, Is.EqualTo(DefaultRawValue));
            }
        }

        [TestFixture]
        internal sealed class ToStringMessage
        {
            [Test]
            public void Same_As_Raw_Value()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.ToString(), Is.EqualTo(DefaultRawValue.ToString()));
            }
        }

        [TestFixture]
        internal sealed class GetHashCodeMessage
        {
            [Test]
            public void Same_As_Raw_Value()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.GetHashCode(), Is.EqualTo(DefaultRawValue.GetHashCode()));
            }
        }

        [TestFixture]
        internal sealed class EqualsMessage
        {
            [Test]
            public void With_Null_Returns_False()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.Equals(null), Is.False);
            }

            [Test]
            public void With_Self_Returns_True()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.Equals(ps), Is.True);
            }

            [TestCase(DefaultRawValue)]
            [TestCase("peter")]
            [TestCase(true)]
            [TestCase(1.25)]
            public void With_Other_Types_Returns_False(object other)
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.Equals(other), Is.False);
            }

            [Test]
            public void With_Same_Value_Returns_True()
            {
                var positiveIntA = new PositiveInteger(DefaultRawValue);
                var positiveIntB = new PositiveInteger(DefaultRawValue);

                Assert.That(positiveIntA.Equals(positiveIntB), Is.True);
            }
        }

        [TestFixture]
        internal sealed class CompareToMessage
        {
            [Test]
            public void With_Null_Returns_Positive()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.CompareTo(null), Is.GreaterThan(0));
            }

            [Test]
            public void With_Self_Returns_Zero()
            {
                var ps = new PositiveInteger(DefaultRawValue);

                Assert.That(ps.CompareTo(ps), Is.Zero);
            }

            [Test]
            public void Same_As_Raw_Value([Values(1, 2)] int rawValueA, [Values(1, 2)] int rawValueB)
            {
                var positiveIntA = new PositiveInteger(rawValueA);
                var positiveIntB = new PositiveInteger(rawValueB);

                Assert.That(positiveIntA.CompareTo(positiveIntB), Is.EqualTo(rawValueA.CompareTo(rawValueB)));
            }
        }
    }
}
