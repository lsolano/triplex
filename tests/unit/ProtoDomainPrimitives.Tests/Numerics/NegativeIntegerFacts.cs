﻿using System;
using NUnit.Framework;
using Triplex.ProtoDomainPrimitives.Exceptions;
using Triplex.ProtoDomainPrimitives.Numerics;

namespace Triplex.ProtoDomainPrimitives.Tests.Numerics
{
    internal static class NegativeIntegerFacts
    {
        private const int DefaultRawValue = -1024;
        private static  readonly Message CustomErrorMessage = new Message("Some dummy error message.");

        internal sealed class ConstructorMessage : RawValueAndErrorMessageBaseFixture
        {
            private readonly Message _expectedErrorMessage;

            public ConstructorMessage(bool useCustomMessage) : base(useCustomMessage)
            {
                _expectedErrorMessage = useCustomMessage ? CustomErrorMessage : NegativeInteger.DefaultErrorMessage;
            }

            [Test]
            public void Rejects_Positives_And_Zero([Values(int.MaxValue, 1, 0)] int rawValue)
                => Assert.That(() => Build(rawValue, UseCustomMessage),
                               Throws.InstanceOf<ArgumentOutOfRangeException>()
                                     .With
                                     .Message
                                     .StartsWith(_expectedErrorMessage.Value));

            [Test]
            public void Accepts_Negatives([Values(-1, DefaultRawValue, int.MinValue)] int rawValue)
                => Assert.That(() => Build(rawValue, UseCustomMessage), Throws.Nothing);
        }

        [TestFixture]
        internal sealed class ConstructorMessageWithInvalidErrorMessage
        {
            [Test]
            public void Rejects_Null_Custom_Error_Message()
            {
                Assert.That(() => new NegativeInteger(DefaultRawValue, null),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("errorMessage"));
            }
        }

        internal sealed class ValueProperty : RawValueAndErrorMessageBaseFixture
        {
            public ValueProperty(bool useCustomMessage) : base(useCustomMessage)
            {
            }

            [Test]
            public void Returns_Constructor_Provided_Value()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.Value, Is.EqualTo(DefaultRawValue));
            }
        }

        internal sealed class ToStringMessage : RawValueAndErrorMessageBaseFixture
        {
            public ToStringMessage(bool useCustomMessage) : base(useCustomMessage)
            {
            }

            [Test]
            public void Same_As_Raw_Value()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.ToString(), Is.EqualTo(DefaultRawValue.ToString()));
            }
        }

        internal sealed class GetHashCodeMessage : RawValueAndErrorMessageBaseFixture
        {
            public GetHashCodeMessage(bool useCustomMessage) : base(useCustomMessage)
            {
            }

            [Test]
            public void Same_As_Raw_Value()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.GetHashCode(), Is.EqualTo(DefaultRawValue.GetHashCode()));
            }
        }

        internal sealed class EqualsMessage : RawValueAndErrorMessageBaseFixture
        {
            public EqualsMessage(bool useCustomMessage) : base(useCustomMessage)
            {
            }

            [Test]
            public void With_Null_Returns_False()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.Equals(null), Is.False);
            }

            [Test]
            public void With_Self_Returns_True()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.Equals(ns), Is.True);
            }

            [TestCase(DefaultRawValue)]
            [TestCase("peter")]
            [TestCase(true)]
            [TestCase(1.25)]
            public void With_Other_Types_Returns_False(object other)
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.Equals(other), Is.False);
            }

            [Test]
            public void With_Same_Value_Returns_True()
            {
                (NegativeInteger positiveIntA, NegativeInteger positiveIntB)
                    = (Build(DefaultRawValue, UseCustomMessage), Build(DefaultRawValue, UseCustomMessage));

                Assert.That(positiveIntA.Equals(positiveIntB), Is.True);
            }

            [Test]
            public void With_Different_Values_Returns_False()
            {
                (NegativeInteger positiveIntA, NegativeInteger positiveIntB)
                    = (Build(DefaultRawValue, UseCustomMessage), Build(DefaultRawValue + 2, UseCustomMessage));

                Assert.That(positiveIntA.Equals(positiveIntB), Is.False);
            }
        }

        internal sealed class CompareToMessage : RawValueAndErrorMessageBaseFixture
        {
            public CompareToMessage(bool useCustomMessage) : base(useCustomMessage)
            {
            }

            [Test]
            public void With_Null_Returns_Positive()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.CompareTo(null), Is.GreaterThan(0));
            }

            [Test]
            public void With_Self_Returns_Zero()
            {
                NegativeInteger ns = Build(DefaultRawValue, UseCustomMessage);

                Assert.That(ns.CompareTo(ns), Is.Zero);
            }

            [Test]
            public void Same_As_Raw_Value([Values(-1, -2)] int rawValueA, [Values(-1, -2)] int rawValueB)
            {
                (NegativeInteger positiveIntA, NegativeInteger positiveIntB)
                    = (Build(rawValueA, UseCustomMessage), Build(rawValueB, UseCustomMessage));

                Assert.That(positiveIntA.CompareTo(positiveIntB), Is.EqualTo(rawValueA.CompareTo(rawValueB)));
            }
        }

        private static NegativeInteger Build(int rawValue, bool useCustomMessage)
        {
            return useCustomMessage ? new NegativeInteger(rawValue, CustomErrorMessage) : new NegativeInteger(rawValue);
        }
    }
}