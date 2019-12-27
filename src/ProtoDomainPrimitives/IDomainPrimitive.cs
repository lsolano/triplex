using System;

namespace Triplex.ProtoDomainPrimitives
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDomainPrimitive<TRawType> : IComparable<IDomainPrimitive<TRawType>>, IEquatable<IDomainPrimitive<TRawType>>
    {
        /// <summary>
        /// 
        /// </summary>
        TRawType Value { get; }
    }
}
