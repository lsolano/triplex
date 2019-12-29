using System;

namespace Triplex.ProtoDomainPrimitives
{
    /// <summary>
    /// 
    /// </summary>
    public class PastOrPresentTimestamp
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultErrorMessage = "'rawValue' must be current system time or some value in the past.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawValue"></param>
        public PastOrPresentTimestamp(DateTimeOffset rawValue)
        {
            throw new ArgumentOutOfRangeException(nameof(rawValue), rawValue, DefaultErrorMessage);
        }

        
    }
}