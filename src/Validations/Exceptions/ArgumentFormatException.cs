using System;
using System.Runtime.Serialization;

namespace Triplex.Validations.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the arguments provided to a method is not valid due to a format
    /// problem, usually refering to a <see cref="String"/> or similar.
    /// </summary>
    [Serializable]
    public sealed class ArgumentFormatException : ArgumentException
    {
        private const string DefaultMessage = "Argument has an invalid format.";

        /// <summary>
        /// 
        /// </summary>
        public ArgumentFormatException()
        {
        }

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
        public ArgumentFormatException(string paramName, Exception innerException)
            : this(paramName, DefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ArgumentFormatException(string paramName, string message)
            : base(paramName: paramName, message: message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ArgumentFormatException(string paramName, string message, Exception innerException)
            : base(paramName: paramName, message: message, innerException: innerException)
        {
        }

        private ArgumentFormatException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
