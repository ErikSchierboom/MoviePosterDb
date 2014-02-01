namespace MoviePosterDb
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// This class offers basic cryptographic functionality extension methods. The class is internal to prevent polluting
    /// the caller's namespace with these extension methods.
    /// </summary>
    internal static class CryptographyExtensions
    {
        /// <summary>
        /// Calculate the MD5 hash of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The MD5 hash.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <c>null</c>.</exception>
        public static string ToMd5(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                var hexadecimalHash = new StringBuilder();

                foreach (var digit in data)
                {
                    hexadecimalHash.Append(digit.ToString("x2"));
                }

                return hexadecimalHash.ToString();
            }
        }
    }
}