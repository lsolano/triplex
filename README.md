# Triplex #
Validation library inspired by the concepts of ***Secure by Design***, by Dan Bergh Johnsson, Daniel Deogun, and Daniel Sawano (MEAP 2019 Manning Publications). (Preconditions, Postconditions, Invariants & Proto-Primitives at [https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts "C# Code Contracts"))


## Types of validations ##
### Arguments ###
Remember that arguments are actual values to methods declared parameters. The utility class used to validate arguments is `Triplex.Validations.Arguments`. If an argument is invalid this utility will throw an `System.ArgumentException` or one of its derivatives.

### Object's state ###
Sometimes we need to check internal state before or after certain operation to ensure preconditions and invariants respectively. To check for those we use `Triplex.Validation.State` utility. This one throws `System.InvalidOperationException` or one of its derivatives.

---

## Examples
### Arguments ###
	using Triplex.Validations;

    public sealed class Email
	{
		private readonly string _value;

		public Email(string value) {
			Arguments.NotNull(value);
			Arguments.LengthBetween(value, 12, 60);
			Arguments.MatchesPattern(value.ToLowerInvariant(), "^[a-zA-Z.]+@toboso.com$");
			
			_value = value.ToLowerInvariant();
		}
	}

All methods has three forms:

1. Data only, no argument name, nor custom message. Like `Arguments.NotNull(someArg);`
2. Data, argument name, but no custom message. Like `Arguments.NotNull(someArg, nameof(someArg));`
3. Data, argument name, and custom message. Like `Arguments.NotNull(someArg, nameof(someArg), "Your custom exception message goes here");`


### Object's state ###
	using Triplex.Validations;

    public sealed class Point
	{
		//private Point stuff here...

		//Useful constructor(s) here
		//public Point(...) { }

		public void MoveBy(int x, int y, int z) {
			//1. Any relevant arguments validations here using Arguments util
			Arguments.All(x, y, z).NotEqualTo(default(int));

			//2. Do your stuff
			ComplexMoveLogic(x, y, z);
			
			//3. Afther moving, could not be outside map's boundaries
			State.StillHolds(_theMap.IsStrictlyWithin(this)); // throws if false
		}
	}

All methods has two forms:

1. Data only, no custom message. Like `State.StillHolds(somePostActionValidation);`
2. Data, argument name, but no custom message. Like `State.StillHolds(somePostActionValidation, "Your custom exception message goes here");`

There is no argument name, because `State` is used to validate objects internal state, as a hole, or invariants.

We like to get help from everybody, if you want to contribute to this tool, found some issue or just want a feature request please read, first, the [Contributing guide](./docs/CONTRIBUTING.md).