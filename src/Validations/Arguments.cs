using System;

namespace Triplex.Validations
{
    /// <summary>
    /// Utility class used to validate arguments. Useful to check constructor and public methods arguments. If checks are violated an instance of <see cref=System.ArgumentException /> is thrown.
    /// </summary>
    public static class Arguments
    {
        /// <summary>
        /// Checks that the provided value is not <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <exception type="System.ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        public static void NotNull(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
        } 
    }
}
