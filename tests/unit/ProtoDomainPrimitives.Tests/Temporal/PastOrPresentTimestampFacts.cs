using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Triplex.ProtoDomainPrimitives.Tests.Temporal
{
    internal static class PastOrPresentTimestampFacts
    {
        public enum TimeMagnitude
        {
            Millisecond = 1,
            Second = 2,
            Minute = 3,
            Hour = 4,
            Day = 5,
            Month = 6,
            Year = 7
        }

        [TestFixture]
        internal sealed class ConstructorMessage
        {
            public static IEnumerable<TimeMagnitude> Magnitudes = Enum.GetValues(typeof(TimeMagnitude)).Cast<TimeMagnitude>().ToList();

            private const string ParamName = "rawValue";
            private const string CustomErrorMessage = "Some custom error message";

            [Test]
            public void Rejects_Future_Values_With_Default_Error_Message(
                [ValueSource(nameof(Magnitudes))] TimeMagnitude timeMagnitude)
            {
                DateTimeOffset rawValue = DateTimeOffset.UtcNow.FromMagnitude(timeMagnitude, 1);

                Assert.That(() => new PastOrPresentTimestamp(rawValue),
                            BuildArgumentOutOfRangeExceptionConstraint(rawValue, PastOrPresentTimestamp.DefaultErrorMessage));
            }

            [Test]
            public void Rejects_Future_Values_With_Custom_Error_Message(
                [ValueSource(nameof(Magnitudes))] TimeMagnitude timeMagnitude)
            {
                DateTimeOffset rawValue = DateTimeOffset.UtcNow.FromMagnitude(timeMagnitude, 1);
                

                Assert.That(() => new PastOrPresentTimestamp(rawValue, CustomErrorMessage),
                            BuildArgumentOutOfRangeExceptionConstraint(rawValue, CustomErrorMessage));
            }

            private static IResolveConstraint BuildArgumentOutOfRangeExceptionConstraint(DateTimeOffset rawValue, string errorMessage)
            {
                return Throws.InstanceOf<ArgumentOutOfRangeException>().With.Message.StartsWith(errorMessage)
                                                                       .And.Message.Contains(ParamName)
                                                                       .And.Message.Contains(rawValue.ToString());
            }

            [Test]
            public void Accepts_Present_Value_With_Default_Error_Message()
                => Assert.That(() => new PastOrPresentTimestamp(DateTimeOffset.UtcNow), Throws.Nothing);

            [Test]
            public void Accepts_Present_Value_With_Custom_Error_Message()
                => Assert.That(() => new PastOrPresentTimestamp(DateTimeOffset.UtcNow, CustomErrorMessage), Throws.Nothing);

            [Test]
            public void Accepts_Past_Values_With_Default_Error_Message([ValueSource(nameof(Magnitudes))] TimeMagnitude timeMagnitude)
            {
                DateTimeOffset rawValue = DateTimeOffset.UtcNow.FromMagnitude(timeMagnitude, -1);

                Assert.That(() => new PastOrPresentTimestamp(rawValue), Throws.Nothing);
            }

            [Test]
            public void Accepts_Past_Values_With_Custom_Error_Message([ValueSource(nameof(Magnitudes))] TimeMagnitude timeMagnitude)
            {
                DateTimeOffset rawValue = DateTimeOffset.UtcNow.FromMagnitude(timeMagnitude, -1);

                Assert.That(() => new PastOrPresentTimestamp(rawValue, CustomErrorMessage), Throws.Nothing);
            }
        }

        private static DateTimeOffset FromMagnitude(this DateTimeOffset offset, TimeMagnitude timeMagnitude, int delta)
            => timeMagnitude switch
            {
                TimeMagnitude.Millisecond => offset.AddMilliseconds(delta),
                TimeMagnitude.Second => offset.AddSeconds(delta),
                TimeMagnitude.Minute => offset.AddMinutes(delta),
                TimeMagnitude.Day => offset.AddDays(delta),
                TimeMagnitude.Hour => offset.AddHours(delta),
                TimeMagnitude.Month => offset.AddMonths(delta),
                TimeMagnitude.Year => offset.AddYears(delta),
                _ => throw new ArgumentOutOfRangeException(nameof(timeMagnitude), timeMagnitude, "Unknown time magnitude.")
            };
    }
}
