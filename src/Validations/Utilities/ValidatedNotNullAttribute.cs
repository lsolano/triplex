using System;

namespace Triplex.Validations.Utilities
{
    // CP to https://github.com/dotnet/corefx/blob/master/src/System.Collections.Immutable/src/Validation/ValidatedNotNullAttribute.cs 2020

    /// <summary>
    /// Indicates to Code Analysis that a method validates a particular parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
