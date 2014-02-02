namespace MoviePosterDb
{
    using System;

    /// <summary>
    /// Utility class used to check parameter values.
    /// </summary>
    internal static class Check
    {
        /// <summary>
        /// Check if an object is not null. If not, throw an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="actual">The parameter value.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="actual"/> is <c>null</c>.</exception>
        public static void NotNull(object actual, string paramName)
        {
            if (actual == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Check if an object is not null or empty. 
        /// If the object is equal to <c>null</c>, throw an <see cref="ArgumentNullException"/>.
        /// If the object is empty, throw an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="actual">The parameter value.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="actual"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="actual"/> is empty.</exception>
        public static void NotNullOrEmpty(string actual, string paramName)
        {
            NotNull(actual, paramName);
            That(!string.IsNullOrEmpty(actual), paramName, string.Format("The {0} parameter must not be empty.", paramName));
        }

        /// <summary>
        /// Check if a condition is true. If not, throw an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="message">The exception message.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="condition"/> is <c>false</c>.</exception>
        public static void That(bool condition, string paramName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Check if a number is within a specified range. If not, throw an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="actual">The parameter value.</param>
        /// <param name="low">The minimum value (inclusive).</param>
        /// <param name="high">The maximum range value (inclusive).</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="actual"/> is not within the specified range.</exception>
        public static void InRange(int actual, int low, int high, string paramName)
        {
            if (actual < low || actual > high)
            {
                throw new ArgumentOutOfRangeException(paramName, actual, string.Format("The {0} parameter must be in the range [{1}-{2}]", paramName, low, high));
            }
        }

        /// <summary>
        /// Check if a number is greater than zero. If not, throw an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="actual">The parameter value.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="actual"/> is not greater than zero.</exception>
        public static void GreaterThanZero(int actual, string paramName)
        {
            if (actual <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, actual, string.Format("The {0} parameter must be greater than zero.", paramName));
            }
        }
    }
}