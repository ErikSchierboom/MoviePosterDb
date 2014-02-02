namespace MoviePosterDb
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// The MoviePosterDb service allows access to the API for the http://www.movieposterdb.com/ website. It allows searching
    /// by IMDb movie ID and returns the movie's information including its posters as stored on MoviePosterDb.
    /// </summary>
    public class MoviePosterDbService
    {
        private const string SearchByImdbMovieIdApiUrl = "http://api.movieposterdb.com/json?imdb_code={0}&api_key={1}&secret={2}&width={3}";
        private const string InvalidImdbMovieUrlMessage = "The URL is not a valid IMDb movie URL.";
        private const int MinimumImageWidth = 30;
        private const int MaximumImageWidth = 300;

        private readonly string apiKey;
        private readonly string apiSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviePosterDbService"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiKey"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiSecret"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="apiKey"/> is empty.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="apiSecret"/> is empty.</exception>
        public MoviePosterDbService(string apiKey, string apiSecret)
        {
            Check.NotNullOrEmpty(apiKey, "apiKey");
            Check.NotNullOrEmpty(apiSecret, "apiSecret");

            this.apiSecret = apiSecret;
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Search for a movie's posters based on an IMDb movie URL.
        /// </summary>
        /// <param name="imdbMovieUrl">The IMDb movie URL.</param>
        /// <param name="imageWidth">The poster image's width to return.</param>
        /// <returns>The API result containing the movie's information and posters.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="imdbMovieUrl"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imdbMovieUrl"/> is not a valid IMDb movie url.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imageWidth"/> is not within the [30-300] range.</exception>
        public MoviePosterDbResult Search(Uri imdbMovieUrl, int imageWidth)
        {
            Check.NotNull(imdbMovieUrl, "imdbMovieUrl");
            Check.That(imdbMovieUrl.IsImdbMovieUrl(), "imdbMovieUrl", InvalidImdbMovieUrlMessage);
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return RequestAndParseApiUrl(this.GetApiUrl(imdbMovieUrl, imageWidth));
        }

        /// <summary>
        /// Search for a movie's posters based on an IMDb movie ID.
        /// </summary>
        /// <param name="imdbMovieId">The IMDb movie ID.</param>
        /// <param name="imageWidth">The poster image's width to return.</param>
        /// <returns>The API result containing the movie's information and posters.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imdbMovieId"/> is not greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imageWidth"/> is not within the [30-300] range.</exception>
        public MoviePosterDbResult Search(int imdbMovieId, int imageWidth)
        {
            Check.GreaterThanZero(imdbMovieId, "imdbMovieId");
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return RequestAndParseApiUrl(this.GetApiUrl(imdbMovieId, imageWidth));
        }

        /// <summary>
        /// Get the API url for an IMDb movie URL search.
        /// </summary>
        /// <param name="imdbMovieUrl">The IMDb movie URL.</param>
        /// <param name="imageWidth">The poster image's width to return.</param>
        /// <returns>The API URL to search for the posters for specified IMDb movie ID.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="imdbMovieUrl"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imdbMovieUrl"/> is not a valid IMDb movie url.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imageWidth"/> is not within the [30-300] range.</exception>
        public Uri GetApiUrl(Uri imdbMovieUrl, int imageWidth)
        {
            Check.NotNull(imdbMovieUrl, "imdbMovieUrl");
            Check.That(imdbMovieUrl.IsImdbMovieUrl(), "imdbMovieUrl", InvalidImdbMovieUrlMessage);
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return this.GetApiUrl(imdbMovieUrl.GetImdbMovieId(), imageWidth);
        }

        /// <summary>
        /// Get the API url for an IMDb movie ID search.
        /// </summary>
        /// <param name="imdbMovieId">The IMDb movie ID.</param>
        /// <param name="imageWidth">The poster image's width to return.</param>
        /// <returns>The API URL to search for the posters for specified IMDb movie ID.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imdbMovieId"/> is not greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imageWidth"/> is not within the [30-300] range.</exception>
        public Uri GetApiUrl(int imdbMovieId, int imageWidth)
        {
            Check.GreaterThanZero(imdbMovieId, "imdbMovieId");
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return new Uri(string.Format(SearchByImdbMovieIdApiUrl, imdbMovieId, this.apiKey, this.CalculateSecret(imdbMovieId), imageWidth));
        }

        /// <summary>
        /// Calculate the API call secret based on an IMDb movie URL.
        /// </summary>
        /// <param name="imdbMovieUrl">The IMDb movie URL.</param>
        /// <returns>The API secret to be used in the API call.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="imdbMovieUrl"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imdbMovieUrl"/> is not a valid IMDb movie url.</exception>
        public string CalculateSecret(Uri imdbMovieUrl)
        {
            Check.NotNull(imdbMovieUrl, "imdbMovieUrl");
            Check.That(imdbMovieUrl.IsImdbMovieUrl(), "imdbMovieUrl", InvalidImdbMovieUrlMessage);

            return this.CalculateSecret(imdbMovieUrl.GetImdbMovieId());
        }

        /// <summary>
        /// Calculate the API call secret based on an IMDb movie ID.
        /// </summary>
        /// <param name="imdbMovieId">The IMDb movie ID.</param>
        /// <returns>The API secret to be used in the API call.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="imdbMovieId"/> is not greater than zero.</exception>
        public string CalculateSecret(int imdbMovieId)
        {
            Check.GreaterThanZero(imdbMovieId, "imdbMovieId");

            return (this.apiSecret + imdbMovieId).ToMd5().Substring(10, 12);
        }

        /// <summary>
        /// Request and parse the API url.
        /// </summary>
        /// <param name="apiUrl">The API url.</param>
        /// <returns>The parsed API response.</returns>
        private static MoviePosterDbResult RequestAndParseApiUrl(Uri apiUrl)
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                return ParseApiResponse(webClient.DownloadData(apiUrl));
            }
        }

        /// <summary>
        /// Parse the API response.
        /// </summary>
        /// <param name="apiResponseData">The API response data</param>
        /// <returns>The parsed API response.</returns>
        private static MoviePosterDbResult ParseApiResponse(byte[] apiResponseData)
        {
            using (var stream = new MemoryStream(apiResponseData))
            {
                var serializer = new DataContractJsonSerializer(typeof(MoviePosterDbResult));
                return serializer.ReadObject(stream) as MoviePosterDbResult;
            }
        }
    }
}