namespace MoviePosterDb
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This class offers extensions to the <see cref="Uri"/> class that allow IMDb url testing and retrieving the IMDb ID from 
    /// an IMDb movie url. The class is internal to prevent polluting the caller's namespace with these extension methods.
    /// </summary>
    internal static class UriExtensions
    {
        private static readonly Regex ImdbMovieUrlRegex = new Regex(@"https?://.*?imdb.com/title/tt(\d{7})/?");

        /// <summary>
        /// Determins if a <see cref="Uri"/> represents an IMDb movie url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns><c>true</c>, when the url is a valid IMDb movie url; otherwise, <c>false</c>.</returns>
        public static bool IsImdbMovieUrl(this Uri url)
        {
            return ImdbMovieUrlRegex.IsMatch(url.AbsoluteUri);
        }

        /// <summary>
        /// Extract the IMDb ID from an IMDb movie url.
        /// </summary>
        /// <example>
        /// Calling <see cref="GetImdbMovieId"/> on the http://www.imdb.com/title/tt1408253/ url will return the number <b>1408253</b>.<br/>
        /// Calling <see cref="GetImdbMovieId"/> on the http://www.imdb.com/title/tt0120586/ url will return the number <b>120586</b>.
        /// </example>
        /// <param name="imdbMovieUrl">The IMDb movie url.</param>
        /// <returns>The IMDb ID.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="imdbMovieUrl"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imdbMovieUrl"/> is not a valid IMDb movie url.</exception>
        public static int GetImdbMovieId(this Uri imdbMovieUrl)
        {
            if (imdbMovieUrl == null)
            {
                throw new ArgumentNullException("imdbMovieUrl");
            }

            var match = ImdbMovieUrlRegex.Match(imdbMovieUrl.AbsoluteUri);

            if (!match.Success)
            {
                throw new ArgumentException("The URL is not a valid IMDb movie URL.", "imdbMovieUrl");
            }

            return Convert.ToInt32(match.Groups[1].Value);
        }
    }
}