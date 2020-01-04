using System;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Triplex.Validations.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the arguments provided to a method is not valid due to a format problem, 
    /// usually refering to a <see cref="String"/> or simmilar.
    /// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
    public sealed class ArgumentFormatException : ArgumentException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        private const string DefaultMessage = "Argument has an invalid format.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        public ArgumentFormatException(string paramName) : base(paramName: paramName, message: DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ArgumentFormatException(string paramName, Exception innerException) : this(paramName, DefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ArgumentFormatException(string paramName, string message) : base(paramName: paramName, message: message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ArgumentFormatException(string paramName, string message, Exception innerException) : base(paramName: paramName, message: message, innerException: innerException)
        {
        }
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters