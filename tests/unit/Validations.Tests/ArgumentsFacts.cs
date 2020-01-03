using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Triplex.Validations.Tests
{
    /// <summary>
    /// Messages variants are: DataOnly (monadic), DataAndName (dyadic), DataNameAndCustomMessage (triadic).
    /// </summary>
#pragma warning disable CA1812 // Allow internal classes not used "anywhere" but by test framework.
    [TestFixture]
    public static class ArgumentsFacts
    {
        private const string DefaultParameterName = "username";
        private const string CustomMessage = "Look caller: the input can't be null.";

        [TestFixture(false)]
        [TestFixture(true)]
        internal abstract class BaseFixture
        {
            protected readonly bool UseCustomErrorMessage;

            protected BaseFixture(bool useCustomErrorMessage) => UseCustomErrorMessage = useCustomErrorMessage;
        }

        internal sealed class NotNullMessage : BaseFixture
        {
            public NotNullMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [Test]
            public void With_Null_Throws_ArgumentNullException(
                [Values("username", "someInput", "iAmAParameter")] string paramName,
                [Values("not good", "check your input")] string customErrorMessage)
            {
                string expectedMessage = BuildFinalMessage(customErrorMessage, paramName, UseCustomErrorMessage);

                Assert.That(() => NotNull((string)null, paramName, customErrorMessage, UseCustomErrorMessage),
                            Throws.ArgumentNullException.With.Message.EqualTo(expectedMessage)
                                .And.Property(nameof(ArgumentNullException.ParamName)).EqualTo(paramName));
            }

            [Test]
            public void With_Peter_Throws_Nothing()
                => Assert.That(() => NotNull("Peter", DefaultParameterName, CustomMessage, UseCustomErrorMessage), Throws.Nothing);

            [TestCase("")]
            [TestCase("peter")]
            [TestCase("spider-man")]
            public void Returns_Value_When_No_Exception_For_Strings(string value)
            {
                string name = NotNull(value, nameof(value), CustomMessage, UseCustomErrorMessage);

                Assert.That(name, Is.EqualTo(value));
            }

            [Test]
            public void Returns_Value_When_No_Exception_For_List()
            {
                var fibonacci = new List<int> {1, 1, 2, 3, 5};
                IList<int> myFibonacci = NotNull(fibonacci, nameof(fibonacci), CustomMessage, UseCustomErrorMessage);

                CollectionAssert.AreEqual(fibonacci, myFibonacci);
            }

#if NETFRAMEWORK
            private static readonly string DefaultErrorTemplate = $"Value cannot be null.{System.Environment.NewLine}Parameter name: {{paramName}}";
            private static string BuildFinalMessage(string customMessagePrefix, string paramName, bool useCustomErrorMessage)
                => useCustomErrorMessage ? $"{customMessagePrefix}{System.Environment.NewLine}Parameter name: {paramName}"
                    : DefaultErrorTemplate.Replace("{paramName}", paramName);
#endif
#if NETCOREAPP
            private const string DefaultErrorTemplate = "Value cannot be null. (Parameter '{paramName}')";
            private static string BuildFinalMessage(string customMessagePrefix, string paramName, bool useCustomErrorMessage)
                => useCustomErrorMessage ? $"{customMessagePrefix} (Parameter '{paramName}')"
                    : DefaultErrorTemplate.Replace("{paramName}", paramName);
#endif
            
            private static TValue NotNull<TValue>(TValue value, string paramName, string customMessage,
                bool useCustomMessage)
                where TValue : class => useCustomMessage
                ? Arguments.NotNull(value, paramName, customMessage)
                : Arguments.NotNull(value, paramName);
        }

        internal sealed class ValidEnumerationMemberMessage : BaseFixture
        {
            public enum Color { Black = 0, White = 1 }

            public ValidEnumerationMemberMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [TestCase(Color.Black)]
            [TestCase(Color.White)]
            public void With_Valid_Constants_Throws_Nothing(Color someColor)
            {
                Assert.That(() => ValidEnumerationMember(someColor, nameof(someColor), null, UseCustomErrorMessage),
                    Throws.Nothing);
            }

            [Test]
            public void With_Invalid_Constants_Throws_ArgumentOutOfRangeException(
                [Values(-1, 2)] Color someColor,
                [Values("this is not OK", "Invalid value")] string customError)
            {
                string expectedMessage = BuildExpectedMessageForValidEnumerationMember(someColor, nameof(Color),
                    nameof(someColor), customError, UseCustomErrorMessage);

                Assert.That(() => ValidEnumerationMember(someColor, nameof(someColor), customError, UseCustomErrorMessage), 
                    Throws.InstanceOf<ArgumentOutOfRangeException>()
                        .With.Message.EqualTo(expectedMessage));
            }

            [TestCase(Color.Black)]
            [TestCase(Color.White)]
            public void With_Valid_Constants_Returns_Value(Color someColor)
            {
                Color validatedColor =
                    ValidEnumerationMember(someColor, nameof(someColor), "some custom error msg", UseCustomErrorMessage);

                Assert.That(validatedColor, Is.EqualTo(someColor));
            }

#if NETFRAMEWORK
            private static readonly string DefaultErrorTemplate = $"Value is not a member of enum {{typeName}}.{System.Environment.NewLine}Parameter name: {{paramName}}{System.Environment.NewLine}Actual value was {{actualValue}}.";
            private static readonly string CustomErrorTemplate = $"{{prefix}}{System.Environment.NewLine}Parameter name: {{paramName}}{System.Environment.NewLine}Actual value was {{actualValue}}.";
#endif
#if NETCOREAPP
            private static readonly string DefaultErrorTemplate = $"Value is not a member of enum {{typeName}}. (Parameter '{{paramName}}'){Environment.NewLine}Actual value was {{actualValue}}.";
            private static readonly string CustomErrorTemplate = $"{{prefix}} (Parameter '{{paramName}}'){Environment.NewLine}Actual value was {{actualValue}}.";
#endif
            private static string BuildExpectedMessageForValidEnumerationMember(object value, string typeName, string paramName, string customErrorMessage, bool useCustomErrorMessage)
            {
                string baseMessage = useCustomErrorMessage
                    ? CustomErrorTemplate.Replace("{prefix}", customErrorMessage)
                    : DefaultErrorTemplate.Replace("{typeName}", typeName);

                return baseMessage.Replace("{paramName}", paramName)
                                  .Replace("{actualValue}", value.ToString());
            }

            private static TValue ValidEnumerationMember<TValue>(TValue value, string paramName, string customMessage,
                bool useCustomMessage) where TValue : Enum
            {
                return useCustomMessage ? Arguments.ValidEnumerationMember(value, paramName, customMessage) : Arguments.ValidEnumerationMember(value, paramName);
            }
        }



        internal sealed class LessThanMessage : BaseFixture
        {
            private const string CustomError = "Must be less than other.";

            public LessThanMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [TestCase(-2,-1)]
            [TestCase(-1,0)]
            [TestCase(0,1)]
            [TestCase(-2,1)]
            public void With_Valid_Integers_Throws_Nothing(int theValue, int other)
            {
                int validatedValue = Arguments.LessThan(theValue, other, nameof(theValue), CustomError);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-2,-1)]
            [TestCase(-1,0)]
            [TestCase(0,1)]
            [TestCase(-2,1)]
            public void With_Valid_Longs_Throws_Nothing(long theValue, long other)
            {
                long validatedValue = LessThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase("a","b")]
            [TestCase("b", "c")]
            [TestCase("a", "c")]
            public void With_Valid_Strings_Throws_Nothing(string theValue, string other)
            {
                string validatedValue = LessThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-2, -2)]
            [TestCase(-1, -2)]
            [TestCase(0, 0)]
            [TestCase(1, 0)]
            [TestCase(2, 2)]
            [TestCase(3, 2)]
            public void With_Equal_Or_Greater_Integers_Throws_ArgumentOutOfRangeException(int theValue, int other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => LessThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            [TestCase("a", "a")]
            [TestCase("b", "a")]
            [TestCase("b", "b")]
            [TestCase("c", "b")]
            [TestCase("c", "c")]
            [TestCase("d", "c")]
            public void With_Equal_Or_Greater_Strings_Throws_ArgumentOutOfRangeException(string theValue, string other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => LessThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            private Constraint BuildConstraint(object theValue, object other, string paramName, string customError)
            {
                Constraint throwsOutOfRange = Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(theValue)
                    .With.Property(nameof(ArgumentException.ParamName)).EqualTo(paramName);

                return UseCustomErrorMessage
                    ? throwsOutOfRange.With.Message.StartWith(customError)
                    : (Constraint) throwsOutOfRange.With.Message.Contains(other.ToString());
            }

            [Test]
            public void With_Null_Value_Throws_ArgumentNullException()
            {
                string someString = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => LessThan(someString, "abc", nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("value"));
            }

            [Test]
            public void With_Null_Other_Throws_ArgumentNullException()
            {
                const string someString = "abc";

                Assert.That(() => LessThan(someString, null, nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("other"));
            }

            [Test]
            public void With_Null_CustomMessage_Throws_ArgumentNullException()
            {
                Assume.That(UseCustomErrorMessage, Is.True);

                const string someString = "abc";

                Assert.That(() => LessThan(someString, "xyz", nameof(someString), null, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
            }

            private static TComparable LessThan<TComparable>(TComparable value, TComparable other, string paramName, string customError, bool useCustomErrorMessage) where TComparable : IComparable<TComparable>
            {
                return useCustomErrorMessage
                    ? Arguments.LessThan(value, other, paramName, customError)
                    : Arguments.LessThan(value, other, paramName);
            }
        }

        internal sealed class LessThanOrEqualToMessage : BaseFixture
        {
            private const string CustomError = "Must be less than or equal other.";

            public LessThanOrEqualToMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [TestCase(-2,-1)]
            [TestCase(-1,-1)]
            [TestCase(-1,0)]
            [TestCase(0,0)]
            [TestCase(0,1)]
            [TestCase(1,1)]
            [TestCase(-2,1)]
            public void With_Valid_Integers_Throws_Nothing(int theValue, int other)
            {
                int validatedValue = Arguments.LessThanOrEqualTo(theValue, other, nameof(theValue), CustomError);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-2,-1)]
            [TestCase(-1,-1)]
            [TestCase(-1,0)]
            [TestCase(0,0)]
            [TestCase(0,1)]
            [TestCase(1,1)]
            [TestCase(-2,1)]
            public void With_Valid_Longs_Throws_Nothing(long theValue, long other)
            {
                long validatedValue = LessThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase("a","b")]
            [TestCase("b","b")]
            [TestCase("b", "c")]
            [TestCase("c", "c")]
            [TestCase("a", "c")]
            public void With_Valid_Strings_Throws_Nothing(string theValue, string other)
            {
                string validatedValue = LessThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-1, -2)]
            [TestCase(1, 0)]
            [TestCase(3, 2)]
            public void With_Greater_Integers_Throws_ArgumentOutOfRangeException(int theValue, int other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => LessThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            [TestCase("b", "a")]
            [TestCase("c", "b")]
            [TestCase("d", "c")]
            public void With_Greater_Strings_Throws_ArgumentOutOfRangeException(string theValue, string other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => LessThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            private Constraint BuildConstraint(object theValue, object other, string paramName, string customError)
            {
                Constraint throwsOutOfRange = Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(theValue)
                    .With.Property(nameof(ArgumentException.ParamName)).EqualTo(paramName);

                return UseCustomErrorMessage
                    ? throwsOutOfRange.With.Message.StartWith(customError)
                    : (Constraint) throwsOutOfRange.With.Message.Contains(other.ToString());
            }

            [Test]
            public void With_Null_Value_Throws_ArgumentNullException()
            {
                string someString = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => LessThanOrEqualTo(someString, "abc", nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("value"));
            }

            [Test]
            public void With_Null_Other_Throws_ArgumentNullException()
            {
                const string someString = "abc";

                Assert.That(() => LessThanOrEqualTo(someString, null, nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("other"));
            }

            [Test]
            public void With_Null_CustomMessage_Throws_ArgumentNullException()
            {
                Assume.That(UseCustomErrorMessage, Is.True);

                const string someString = "abc";

                Assert.That(() => LessThanOrEqualTo(someString, "xyz", nameof(someString), null, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
            }

            private static TComparable LessThanOrEqualTo<TComparable>(TComparable value, TComparable other, string paramName, string customError, bool useCustomErrorMessage) where TComparable : IComparable<TComparable>
            {
                return useCustomErrorMessage
                    ? Arguments.LessThanOrEqualTo(value, other, paramName, customError)
                    : Arguments.LessThanOrEqualTo(value, other, paramName);
            }
        }

        //GreaterThan
        internal sealed class GreaterThanMessage : BaseFixture
        {
            private const string CustomError = "Must be less than or equal other.";

            public GreaterThanMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [TestCase(-1,-2)]
            [TestCase(0,-1)]
            [TestCase(1,0)]
            [TestCase(2,1)]
            [TestCase(2,-1)]
            public void With_Valid_Integers_Throws_Nothing(int theValue, int other)
            {
                int validatedValue = Arguments.GreaterThan(theValue, other, nameof(theValue), CustomError);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-1,-2)]
            [TestCase(0,-1)]
            [TestCase(1,0)]
            [TestCase(2,1)]
            [TestCase(2,-1)]
            public void With_Valid_Longs_Throws_Nothing(long theValue, long other)
            {
                long validatedValue = GreaterThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase("b","a")]
            [TestCase("c","b")]
            public void With_Valid_Strings_Throws_Nothing(string theValue, string other)
            {
                string validatedValue = GreaterThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-2, -1)]
            [TestCase(-1, 0)]
            [TestCase(-2, 2)]
            [TestCase(-2, -2)]
            [TestCase(2, 2)]
            public void With_Less_Or_Equal_Integers_Throws_ArgumentOutOfRangeException(int theValue, int other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => GreaterThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            [TestCase("a", "b")]
            [TestCase("b", "c")]
            [TestCase("a", "c")]
            [TestCase("a", "a")]
            [TestCase("c", "c")]
            public void With_Less_Or_Equal_Strings_Throws_ArgumentOutOfRangeException(string theValue, string other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => GreaterThan(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            private Constraint BuildConstraint(object theValue, object other, string paramName, string customError)
            {
                Constraint throwsOutOfRange = Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(theValue)
                    .With.Property(nameof(ArgumentException.ParamName)).EqualTo(paramName);

                return UseCustomErrorMessage
                    ? throwsOutOfRange.With.Message.StartWith(customError)
                    : (Constraint) throwsOutOfRange.With.Message.Contains(other.ToString());
            }

            [Test]
            public void With_Null_Value_Throws_ArgumentNullException()
            {
                string someString = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => GreaterThan(someString, "abc", nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("value"));
            }

            [Test]
            public void With_Null_Other_Throws_ArgumentNullException()
            {
                const string someString = "abc";

                Assert.That(() => GreaterThan(someString, null, nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("other"));
            }

            [Test]
            public void With_Null_CustomMessage_Throws_ArgumentNullException()
            {
                Assume.That(UseCustomErrorMessage, Is.True);

                const string someString = "abc";

                Assert.That(() => GreaterThan(someString, "xyz", nameof(someString), null, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
            }

            private static TComparable GreaterThan<TComparable>(TComparable value, TComparable other, string paramName, string customError, bool useCustomErrorMessage) where TComparable : IComparable<TComparable>
            {
                return useCustomErrorMessage
                    ? Arguments.GreaterThan(value, other, paramName, customError)
                    : Arguments.GreaterThan(value, other, paramName);
            }
        }

        //GreaterThanOrEqualTo
        internal sealed class GreaterThanOrEqualToMessage : BaseFixture
        {
            private const string CustomError = "Must be less than or equal other.";

            public GreaterThanOrEqualToMessage(bool useCustomErrorMessage) : base(useCustomErrorMessage)
            {
            }

            [TestCase(-2,-2)]
            [TestCase(-1,-2)]
            [TestCase(0,-1)]
            [TestCase(1,0)]
            [TestCase(2,1)]
            [TestCase(2,2)]
            [TestCase(2,-2)]
            public void With_Valid_Integers_Throws_Nothing(int theValue, int other)
            {
                int validatedValue = Arguments.GreaterThanOrEqualTo(theValue, other, nameof(theValue), CustomError);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-2,-2)]
            [TestCase(-1,-2)]
            [TestCase(0,-1)]
            [TestCase(1,0)]
            [TestCase(2,1)]
            [TestCase(2,2)]
            [TestCase(2,-2)]
            public void With_Valid_Longs_Throws_Nothing(long theValue, long other)
            {
                long validatedValue = GreaterThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase("b","a")]
            [TestCase("c","b")]
            [TestCase("a","a")]
            [TestCase("c","c")]
            public void With_Valid_Strings_Throws_Nothing(string theValue, string other)
            {
                string validatedValue = GreaterThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage);

                Assert.That(validatedValue, Is.EqualTo(theValue));
            }

            [TestCase(-1, 0)]
            [TestCase(-1, 1)]
            public void With_Less_Integers_Throws_ArgumentOutOfRangeException(int theValue, int other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => GreaterThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            [TestCase("a", "b")]
            [TestCase("b", "c")]
            [TestCase("a", "c")]
            public void With_Less_Strings_Throws_ArgumentOutOfRangeException(string theValue, string other)
            {
                Constraint throwsOutOfRange = BuildConstraint(theValue, other, nameof(theValue), CustomError);
                Assert.That(() => GreaterThanOrEqualTo(theValue, other, nameof(theValue), CustomError, UseCustomErrorMessage),
                    throwsOutOfRange);
            }

            private Constraint BuildConstraint(object theValue, object other, string paramName, string customError)
            {
                Constraint throwsOutOfRange = Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property(nameof(ArgumentOutOfRangeException.ActualValue)).EqualTo(theValue)
                    .With.Property(nameof(ArgumentException.ParamName)).EqualTo(paramName);

                return UseCustomErrorMessage
                    ? throwsOutOfRange.With.Message.StartWith(customError)
                    : (Constraint) throwsOutOfRange.With.Message.Contains(other.ToString());
            }

            [Test]
            public void With_Null_Value_Throws_ArgumentNullException()
            {
                string someString = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => GreaterThanOrEqualTo(someString, "abc", nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("value"));
            }

            [Test]
            public void With_Null_Other_Throws_ArgumentNullException()
            {
                const string someString = "abc";

                Assert.That(() => GreaterThanOrEqualTo(someString, null, nameof(someString), CustomError, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("other"));
            }

            [Test]
            public void With_Null_CustomMessage_Throws_ArgumentNullException()
            {
                Assume.That(UseCustomErrorMessage, Is.True);

                const string someString = "abc";

                Assert.That(() => GreaterThanOrEqualTo(someString, "xyz", nameof(someString), null, UseCustomErrorMessage),
                    Throws.ArgumentNullException
                        .With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("customMessage"));
            }

            private static TComparable GreaterThanOrEqualTo<TComparable>(TComparable value, TComparable other, string paramName, string customError, bool useCustomErrorMessage) where TComparable : IComparable<TComparable>
            {
                return useCustomErrorMessage
                    ? Arguments.GreaterThanOrEqualTo(value, other, paramName, customError)
                    : Arguments.GreaterThanOrEqualTo(value, other, paramName);
            }
        }
    }
#pragma warning restore CA1812 // Allow internal classes not used "anywhere" but by test framework.
}
