﻿using System;

namespace Triplex.ProtoDomainPrimitives
{
    /// <summary>
    /// Useful base class when the domain primitive will be a proxy of the wrapped value. All operations, except input validation, are based on the wrapped type.
    /// </summary>
    /// <typeparam name="TRawType">Wrapped type</typeparam>
    public abstract class AbstractDomainPrimitive<TRawType> : IDomainPrimitive<TRawType> where TRawType : IComparable<TRawType>, IEquatable<TRawType>
    {
        /// <summary>
        /// Builds a new instance calling <paramref name="validator"/> first.
        /// </summary>
        /// <param name="rawValue">Value to wrap.</param>
        /// <param name="validator">Validator function, must perform all validations and throw exceptions, if everything is OK returns the <paramref name="rawValue"/></param>
        /// <exception cref="ArgumentNullException">When <paramref name="validator"/> is <see langword="null"/>.</exception>
        protected AbstractDomainPrimitive(TRawType rawValue, Func<TRawType, TRawType> validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            Value = validator(rawValue);
        }

        /// <summary>
        /// Wrapped value.
        /// </summary>
        public TRawType Value { get; }

        /// <summary>
        /// Calls <see cref="IEquatable{TRawType}.Equals(TRawType)"/> casting <paramref name="obj"/> before.
        /// </summary>
        /// <param name="obj">Comparison target.</param>
        /// <returns></returns>
        public override bool Equals(object obj) => Equals(obj as AbstractDomainPrimitive<TRawType>);

        /// <summary>
        /// Same as <see cref="Value"/>'s hash-code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Same as <see cref="Value"/>'s ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IDomainPrimitive<TRawType> other)
        {
            if (other == null)
            {
                return 1;
            }

            return ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Same as wrapped instances equals.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDomainPrimitive<TRawType>? other)
        {
            if (other == null)
            {
                return false;
            }

            // ReSharper disable once SimplifyConditionalTernaryExpression
            return ReferenceEquals(this, other) ? true : Value.Equals(other.Value);
        }
    }
}