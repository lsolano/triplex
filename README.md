# Triplex #
Validation library inspired by the concepts of ***Secure by Design***, by Dan Bergh Johnsson, Daniel Deogun, and Daniel Sawano (MEAP 2019 Manning Publications).


## Types of validations ##
### Arguments ###
Remember that arguments are actual values to methods declared parameters. The utility class used to validate arguments is `Triplex.Validations.Arguments`. If an argument is invalid this utility will throw an `System.ArgumentException` or one of its derivatives.

### Object's state ###
Sometimes we need to check internal state before or after certain operation to ensure preconditions and invariants respectively. To check for those we use Triplex.Validation.State utility. This one throws `System.InvalidOperationException` or one of its derivatives.

---

## Examples
### Arguments ###
	using Triplex.Validations;

    public sealed class Email
	{
		private readonly string _value;

		public Email(string value) {
			Arguments.NotNull(value, nameof(value));
			Arguments.LengthBetween(value, nameof(value), 12, 60);
			Arguments.MatchesPattern(value.ToLowerInvariant(), "^[a-zA-Z.]+@toboso.com$");
		}
	}
### Object's state ###

---
We like to get help from everybody, if you want to contribute to this tool, found some issue or just want a feature request please read, first, the [Contributing guide](.\docs\CONTRIBUTING.md).