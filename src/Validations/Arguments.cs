using System;

namespace Triplex.Validations
{
    /// <summary>
    /// Utility class used to validate arguments. Useful to check constructor and public methods arguments.
    /// If checks are violated an instance of <see cref="System.ArgumentException" /> is thrown.
    /// </summary>
    public static class Arguments
    {
        /// <summary>
        /// 
        /// Same as <see cref="NotNull(object, string)" /> without (<see langword="null" />) <code>paramName</code>.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <exception type="System.ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        public static void NotNull(object value) => NotNull(value, null);

        /// <summary>
        /// Same as <see cref="NotNull(object, string, string)"/> without (<see langword="null" />) <code>customMessage</code>.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Used to build exception message in case check fails</param>
        /// <exception type="System.ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        public static void NotNull(object value, string paramName) => NotNull(value, paramName, null);

        /// <summary>
        /// Checks that the provided value is not <see langword="null" />.
        /// </summary>
        /// <remarks>
        /// <br>* Certain derivatives from <see cref="System.ArgumentException" /> such as <see cref="System.ArgumentNullException" /> does not allow direc message setting.</br>
        /// <br>This one will use the provided message and used to build a final one.</br> 
        /// </remarks>
        /// <param name="value">Value to check</param>
        /// <param name="paramName">Used to build exception message in case check fails</param>
        /// <param name="customMessage">Custom message, or message *prefix</param>
        /// <exception type="System.ArgumentNullException">If <paramref name="value"/> is <see langword="null" />.</exception>
        public static void NotNull(object value, string paramName, string customMessage)
        {
            if (value != null)
            {
                return;
            }

            ThrowArgumentNullException(paramName, customMessage);
        }

        private static void ThrowArgumentNullException(string paramName, string customMessage)
        {
            if (customMessage == null)
            {
                throw new ArgumentNullException(paramName: paramName);
            }

            throw new ArgumentNullException(message: customMessage, paramName: paramName);
        }

        //private static void somethingnotused() { }
    }
}
