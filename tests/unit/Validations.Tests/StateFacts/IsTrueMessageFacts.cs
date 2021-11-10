namespace Triplex.Validations.Tests.StateFacts
{
    [TestFixture]
    internal sealed class IsTrueMessageFacts
    {
        [Test]
        public void With_Null_Message_Throws_ArgumentNullException([Values] bool stateQuery) {
            const string argumentName = "message";
            Assert.That(() => State.IsTrue(stateQuery, message: null!), 
                Throws.ArgumentNullException
                      .With.Message.Contains(argumentName)
                      .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo(argumentName));
        }

        [Test]
        public void With_True_StateQuery_And_Not_Null_Message_Throws_Nothing()
            => Assert.That(() => State.IsTrue(true, "Message if not true."), Throws.Nothing);

        [Test]
        public void With_False_StateQuery_And_Not_Null_Message_Throws_InvalidOperationException()
        {
            const string theMessage = "Message if not true.";
            
            Assert.That(() => State.IsTrue(false, theMessage),
                Throws.InvalidOperationException.With.Message.EqualTo(theMessage));
        }
    }
}
