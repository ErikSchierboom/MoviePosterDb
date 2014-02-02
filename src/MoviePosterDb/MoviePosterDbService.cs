namespace MoviePosterDb
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class MoviePosterDbService
    {
        private const string SearchByImdbMovieIdApiUrl = "http://api.movieposterdb.com/json?imdb_code={0}&api_key={1}&secret={2}&width={3}";
        private const string InvalidImdbMovieUrlMessage = "The URL is not a valid IMDb movie URL.";
        private const int MinimumImageWidth = 30;
        private const int MaximumImageWidth = 300;

        private readonly string apiKey;
        private readonly string apiSecret;

        public MoviePosterDbService(string apiKey, string apiSecret)
        {
            Check.NotNullOrEmpty(apiKey, "apiKey");
            Check.NotNullOrEmpty(apiSecret, "apiSecret");

            this.apiSecret = apiSecret;
            this.apiKey = apiKey;
        }

        public MoviePosterDbResult Search(int imdbMovieId, int imageWidth)
        {
            Check.GreaterThanZero(imdbMovieId, "imdbMovieId");
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return RequestAndParseApiUrl(this.GetApiUrl(imdbMovieId, imageWidth));
        }

        public MoviePosterDbResult Search(Uri imdbMovieUrl, int imageWidth)
        {
            Check.NotNull(imdbMovieUrl, "imdbMovieUrl");
            Check.That(imdbMovieUrl.IsImdbMovieUrl(), "imdbMovieUrl", InvalidImdbMovieUrlMessage);
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return RequestAndParseApiUrl(this.GetApiUrl(imdbMovieUrl, imageWidth));
        }

        public Uri GetApiUrl(Uri imdbMovieUrl, int imageWidth)
        {
            Check.NotNull(imdbMovieUrl, "imdbMovieUrl");
            Check.That(imdbMovieUrl.IsImdbMovieUrl(), "imdbMovieUrl", InvalidImdbMovieUrlMessage);
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return this.GetApiUrl(imdbMovieUrl.GetImdbMovieId(), imageWidth);
        }

        public Uri GetApiUrl(int imdbMovieId, int imageWidth)
        {
            Check.GreaterThanZero(imdbMovieId, "imdbMovieId");
            Check.InRange(imageWidth, MinimumImageWidth, MaximumImageWidth, "imageWidth");

            return new Uri(string.Format(SearchByImdbMovieIdApiUrl, imdbMovieId, this.apiKey, this.CalculateSecret(imdbMovieId), imageWidth));
        }

        public string CalculateSecret(Uri imdbMovieUrl)
        {
            Check.NotNull(imdbMovieUrl, "imdbMovieUrl");
            Check.That(imdbMovieUrl.IsImdbMovieUrl(), "imdbMovieUrl", InvalidImdbMovieUrlMessage);

            return this.CalculateSecret(imdbMovieUrl.GetImdbMovieId());
        }

        public string CalculateSecret(int imdbMovieId)
        {
            Check.GreaterThanZero(imdbMovieId, "imdbMovieId");

            return (this.apiSecret + imdbMovieId).ToMd5().Substring(10, 12);
        }

        private static MoviePosterDbResult RequestAndParseApiUrl(Uri apiUrl)
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                return ParseApiResponse(webClient.DownloadData(apiUrl));
            }
        }

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