[assembly: System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]

namespace Triplex.Validations.Tests;

/// <summary>
/// Utility to help build NUnit constraints.
/// </summary>
public static class FluentConstraintsExtensions
{
    /// <summary>
    /// Creates a constraint for Message:string property containing all the enumerated parts. 
    /// </summary>
    /// <param name="exceptionConstraint">Base exception constraint</param>
    /// <param name="expectedParts">
    /// Expected parts to be found inside the <see cref="Exception.Message"/> property.
    /// </param>
    /// <returns></returns>
    public static Constraint WithMessageContainsAll(this TypeConstraint exceptionConstraint,
        IEnumerable<string> expectedParts)
    {
        ConstraintExpression messageExpression = exceptionConstraint.With;
        string[] parts = [.. expectedParts];
        for (int i = 0; i < parts.Length - 1; i++)
        {
            messageExpression = messageExpression.Message.Contains(parts[i]).And;
        }

        return messageExpression.Message.Contains(parts[^1]);
    }
}
