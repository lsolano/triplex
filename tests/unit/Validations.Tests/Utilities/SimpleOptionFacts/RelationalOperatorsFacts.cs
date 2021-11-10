using NUnit.Framework;
using Triplex.Validations.Utilities;

namespace Triplex.Validations.Tests.Utilities.SimpleOptionFacts
{
    [TestFixture]
    internal sealed class RelationalOperatorsFacts
    {
        #region Equals operator ==
        [Test]
        public void With_Some_Empty_Returns_False([Values] bool firstFull)
        {
            SimpleOption<string> a = FullOrEmptyOption(firstFull);
            SimpleOption<string> b = FullOrEmptyOption(!firstFull);

            Assert.That(TestEquals(a, b), Is.False);
        }

        [Test]
        public void With_Both_Non_Empty_And_Different_Values_Returns_False()
        {
            SimpleOption<string> a = SimpleOption.SomeNotNull("Cat");
            SimpleOption<string> b = SimpleOption.SomeNotNull("Dog");

            Assert.That(TestEquals(a, b), Is.False);
        }

        [Test]
        public void With_Both_Non_Empty_And_Same_Values_Returns_True()
        {
            SimpleOption<int> a = SimpleOption.SomeNotNull(1_024);
            SimpleOption<int> b = SimpleOption.SomeNotNull(1_024);

            Assert.That(TestEquals(a, b), Is.True);
        }

        [Test]
        public void With_Both_Empty_Returns_True()
        {
            SimpleOption<int> a = SimpleOption.None<int>();
            SimpleOption<int> b = SimpleOption.None<int>();

            Assert.That(TestEquals(a, b), Is.True);
        }
        #endregion //Equals operator ==

        #region Not Equals operator !=
        [Test]
        public void With_Some_Empty_Returns_True([Values] bool firstFull)
        {
            SimpleOption<string> a = FullOrEmptyOption(firstFull);
            SimpleOption<string> b = FullOrEmptyOption(!firstFull);

            Assert.That(TestNotEquals(a, b), Is.True);
        }

        [Test]
        public void With_Both_Non_Empty_And_Different_Values_Returns_True()
        {
            SimpleOption<string> a = SimpleOption.SomeNotNull("Cat");
            SimpleOption<string> b = SimpleOption.SomeNotNull("Dog");

            Assert.That(TestNotEquals(a, b), Is.True);
        }

        [Test]
        public void With_Both_Non_Empty_And_Same_Values_Returns_False()
        {
            SimpleOption<int> a = SimpleOption.SomeNotNull(1_024);
            SimpleOption<int> b = SimpleOption.SomeNotNull(1_024);

            Assert.That(TestNotEquals(a, b), Is.False);
        }

        [Test]
        public void With_Both_Empty_Returns_False()
        {
            SimpleOption<int> a = SimpleOption.None<int>();
            SimpleOption<int> b = SimpleOption.None<int>();

            Assert.That(TestNotEquals(a, b), Is.False);
        }
        #endregion //Not Equals operator !=

        private static bool TestEquals<T>(SimpleOption<T> a, SimpleOption<T> b) => a == b;
        private static bool TestNotEquals<T>(SimpleOption<T> a, SimpleOption<T> b) => a != b;

        private static SimpleOption<string> FullOrEmptyOption(bool full)
        {
            const string value = "Hello Options";

            return full ? SimpleOption.SomeNotNull(value) : SimpleOption.None<string>();
        }
    }
}
