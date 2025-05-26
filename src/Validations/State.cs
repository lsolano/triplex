using System.Runtime.CompilerServices;

namespace Triplex.Validations;

#if (NETSTANDARD || NETCOREAPP)
#pragma warning disable CS0436 //CallerArgumentExpressionAttribute type conflicts
#endif

/// <summary>
/// Object internal state checks. Use it to check Preconditions and Invariants. 
/// Always throw <see cref="InvalidOperationException"/> or some derivative.
/// </summary>
public static class State
{
    #region Preconditions

    /// <summary>
    /// Throws if <paramref name="stateQuery" /> is <code>false</code>.
    /// </summary>
    /// <param name="stateQuery">Precondition to check.</param>
    /// <param name="message">Can not be <see langword="null"/></param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static void IsTrue(bool stateQuery, [NotNull] string message)
    {
        string notNullMessage = NullAndEmptyChecks.NotNull(message, nameof(message));

        if (!stateQuery)
        {
            throw new InvalidOperationException(notNullMessage);
        }
    }

    /// <summary>
    /// Throws if <paramref name="stateQuery" /> is <code>true</code>.
    /// </summary>
    /// <param name="stateQuery">Precondition to check.</param>
    /// <param name="message">Can not be <see langword="null"/></param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static void IsFalse(bool stateQuery, [NotNull] string message)
    {
        NullAndEmptyChecks.NotNull(message, nameof(message));

        if (stateQuery)
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// Object state part null check.
    /// </summary>
    /// <param name="stateElement">State part to check for <see langword="null"/></param>
    /// <param name="elementName">Can not be <see langword="null"/></param>
    /// <returns> <paramref name="stateElement"/> or throws <see cref="ArgumentNullException"/> </returns>
    /// <exception cref="ArgumentNullException">
    /// When <paramref name="elementName"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// When <paramref name="stateElement"/> is <see langword="null"/>.
    /// </exception>
    [DebuggerStepThrough]
    [return: NotNull]
    public static T IsNotNull<T>(
        [NotNull] T stateElement,
        [NotNull, CallerArgumentExpression("stateElement")] string elementName = "")
    {
        NullAndEmptyChecks.NotNull(elementName, nameof(elementName));

        return stateElement.ValueOrThrowInvalidOperationIfNull(elementName);
    }

    #endregion //Preconditions


    #region Invariants

    /// <summary>
    /// Throws if <paramref name="invariant" /> is <code>false</code>.
    /// </summary>
    /// <param name="invariant">Expected to be <code>true</code></param>
    /// <param name="message">Can not be <see langword="null"/></param>
    [DebuggerStepThrough]
    public static void StillHolds(bool invariant, [NotNull] string message)
    {
        NullAndEmptyChecks.NotNull(message, nameof(message));

        if (!invariant)
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// Throws if <paramref name="invariant" /> is <code>true</code>.
    /// </summary>
    /// <param name="invariant">Expected to be <code>false</code></param>
    /// <param name="message">Can not be <see langword="null"/></param>
    [DebuggerStepThrough]
    public static void StillNotHolds(bool invariant, [NotNull] string message)
    {
        NullAndEmptyChecks.NotNull(message, nameof(message));

        if (invariant)
        {
            throw new InvalidOperationException(message);
        }
    }

    #endregion //Invariants
}

#if (NETSTANDARD || NETCOREAPP)
#pragma warning restore CS0436 //CallerArgumentExpressionAttribute type conflicts
#endif