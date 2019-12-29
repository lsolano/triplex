using NUnit.Framework;
using System;

namespace Triplex.ProtoDomainPrimitives.Tests.Temporal
{
    internal static class PastOrPresentTimestampFacts
    {
        public enum TimeMagnitude
        {
            Tick = 0,
            Millisecond = 1,
            Second = 2,
            Minute = 3,
            Hour = 4,
            Day = 5,
            Month = 6,
            Year = 7
        }

        /*
         * Must allow only present or past timestamps.

         * 6. Accepts current date time
         * 7. Accepts 30 seconds in the past
         * 8. Accepts 1 minute in the past
         * 9. Accepts 1 hour in the past
         * 10. Accepts 1 day in the past
         * 11. Accepts 1 month in the past
         * 12. Accepts 1 year in the past
         * 
         */

        [TestFixture]
        internal sealed class ConstructorMessage
        {
            [TestCase(TimeMagnitude.Second, 30)]
            [TestCase(TimeMagnitude.Minute, 1)]
            [TestCase(TimeMagnitude.Hour, 1)]
            [TestCase(TimeMagnitude.Day, 1)]
            [TestCase(TimeMagnitude.Month, 1)]
            [TestCase(TimeMagnitude.Year, 1)]
            public void Rejects_30_Seconds_In_The_Future(TimeMagnitude timeMagnitude, int delta)
            {
                DateTimeOffset rawValue = DateTimeOffset.UtcNow.FromMagnitude(timeMagnitude, delta);

                Assert.That(() => new PastOrPresentTimestamp(rawValue), Throws.InstanceOf<ArgumentOutOfRangeException>()
                                                                              .With.Message.StartsWith(PastOrPresentTimestamp.DefaultErrorMessage)
                                                                              .And.Message.Contains("rawValue")
                                                                              .And.Message.Contains(rawValue.ToString()));
            }
        }

        private static DateTimeOffset FromMagnitude(this DateTimeOffset offset, TimeMagnitude timeMagnitude, int delta)
            => timeMagnitude switch
            {
                TimeMagnitude.Tick => offset.AddTicks(delta),
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
