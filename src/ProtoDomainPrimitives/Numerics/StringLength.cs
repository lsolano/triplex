using System;
using Triplex.ProtoDomainPrimitives.Exceptions;

namespace Triplex.ProtoDomainPrimitives.Numerics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StringLength : AbstractDomainPrimitive<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Message DefaultErrorMessage = new Message("'rawValue' must be zero or positive.");

        /// <summary>
        /// 
        /// </summary>
        public static readonly  StringLength Min = new StringLength(0);

        /// <summary>
        /// 
        /// </summary>
        public static readonly  StringLength Max = new StringLength(int.MaxValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawValue"></param>
        public StringLength(int rawValue) : this(rawValue, DefaultErrorMessage)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawValue"></param>
        /// <param name="errorMessage"></param>
        public StringLength(int rawValue, Message errorMessage) : base(rawValue, errorMessage, Validate)
        {
        }

        private static int Validate(int rawValue, Message errorMessage)
        {
            if (rawValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rawValue), rawValue, errorMessage.Value);
            }

            return rawValue;
        }
    }
}
