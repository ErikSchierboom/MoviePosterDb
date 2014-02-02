namespace MoviePosterDb
{
    using System;

    internal static class Check
    {
        public static void NotNull(object actual, string paramName)
        {
            if (actual == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void That(bool condition, string paramName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }

        public static void NotNullOrEmpty(string actual, string paramName)
        {
            NotNull(actual, paramName);
            That(!string.IsNullOrEmpty(actual), paramName, string.Format("The {0} parameter must not be empty.", paramName));
        }

        public static void InRange(int actual, int low, int high, string paramName)
        {
            if (actual < low || actual > high)
            {
                throw new ArgumentOutOfRangeException(paramName, actual, string.Format("The {0} parameter must be in the range [{1}-{2}]", paramName, low, high));
            }
        }

        public static void GreaterThanZero(int actual, string paramName)
        {
            if (actual <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, actual, string.Format("The {0} parameter must be greater than zero.", paramName));
            }
        }
    }
}