using System;
using System.Diagnostics;

using Triplex.Validations.Utilities;

namespace Triplex.Validations
{
    /// <summary>
    /// Object internal state checks. Use it to check Preconditions and Invariants. 
    /// Always throw <see cref="System.InvalidOperationException"/> or some derivative.
    /// </summary>
    public sealed class State
    {
        #region Preconditions

        /// <summary>
        /// Throws if <paramref name="stateQuery" /> is <code>false</code>.
        /// </summary>
        /// <param name="stateQuery">Precondition to check.</param>
        /// <param name="message">Can not be <see langword="null"/></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static void IsTrue(in bool stateQuery, [ValidatedNotNull] in string message) {
            Arguments.NotNull(message, nameof(message));
            
            if (!stateQuery) {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Throws if <paramref name="stateQuery" /> is <code>true</code>.
        /// </summary>
        /// <param name="stateQuery">Precondition to check.</param>
        /// <param name="message">Can not be <see langword="null"/></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static void IsFalse(in bool stateQuery, [ValidatedNotNull] in string message) {
            Arguments.NotNull(message, nameof(message));

            if (stateQuery) {
                throw new InvalidOperationException(message);
            }
        }

        #endregion //Preconditions


        #region Invariants

        /// <summary>
        /// Throws if <paramref name="invariant" /> is <code>false</code>.
        /// </summary>
        /// <param name="invariant">Expected to be <code>true</code></param>
        /// <param name="message">Can not be <see langword="null"/></param>
        [DebuggerStepThrough]
        public static void StillHolds(in bool invariant, [ValidatedNotNull] in string message) {
            Arguments.NotNull(message, nameof(message));

            if (!invariant) {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Throws if <paramref name="invariant" /> is <code>true</code>.
        /// </summary>
        /// <param name="invariant">Expected to be <code>false</code></param>
        /// <param name="message">Can not be <see langword="null"/></param>
        [DebuggerStepThrough]
        public static void StillNotHolds(in bool invariant, [ValidatedNotNull] in string message) {
            Arguments.NotNull(message, nameof(message));

            if (invariant) {
                throw new InvalidOperationException(message);
            }
        }

        #endregion //Invariants
    }
}