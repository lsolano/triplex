using Triplex.ProtoDomainPrimitives.Numerics;

namespace Triplex.ProtoDomainPrimitives.Strings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoundedLengthString : IDomainPrimitive<string>
    {
        /// <summary>
        /// 
        /// </summary>
        StringLength MinLength { get; }

        /// <summary>
        /// 
        /// </summary>
        StringLength MaxLength { get; }
    }
}
