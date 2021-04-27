using System;
using NUnit.Framework;

namespace Triplex.Validations.Tests.StateFacts
{
    [TestFixture]
    internal sealed class StillNotHoldsMessageFacts
    {
        [Test]
        public void With_Null_Message_Throws_ArgumentNullException([Values] bool invariant) {
            const string argumentName = "message";
            Assert.That(() => State.StillNotHolds(invariant, message: null), 
                Throws.ArgumentNullException
                      .With.Message.Contains(argumentName)
                      .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo(argumentName));
        }

        [Test]
        public void With_False_Invariant_And_Not_Null_Message_Throws_Nothing()
            => Assert.That(() => State.StillNotHolds(false, "Message if not true."), Throws.Nothing);

        [Test]
        public void With_True_Invariant_And_Not_Null_Message_Throws_InvalidOperationException()
        {
            const string theMessage = "Message if not true.";
            Assert.That(() => State.StillNotHolds(true, theMessage),
                Throws.InvalidOperationException.With.Message.EqualTo(theMessage));
        }
    }
}