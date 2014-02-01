namespace MoviePosterDb
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class MoviePosterDbService
    {
        private const string ApiUrl = "http://api.movieposterdb.com/json?imdb_code={0}&api_key={1}&secret={2}&width={3}";

        private readonly string apiKey;
        private readonly string apiSecret;

        public MoviePosterDbService(string apiKey, string apiSecret)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException("apiKey");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("The API key must not be empty.", "apiKey");
            }

            if (apiSecret == null)
            {
                throw new ArgumentNullException("apiSecret");
            }

            if (string.IsNullOrEmpty(apiSecret))
            {
                throw new ArgumentException("The API secret must not be empty.", "apiSecret");
            }

            this.apiSecret = apiSecret;
            this.apiKey = apiKey;
        }

        public Uri GetPosterUrl(string imdbMovieUrl, int imageWidth)
        {
            return this.GetPosterUrl(new Uri(imdbMovieUrl), imageWidth);
        }

        public Uri GetPosterUrl(Uri imdbMovieUrl, int imageWidth)
        {
            if (imdbMovieUrl == null)
            {
                throw new ArgumentNullException("imdbMovieUrl");
            }

            if (!imdbMovieUrl.IsImdbMovieUrl())
            {
                throw new ArgumentException("The URL is not a valid IMDb movie URL.", "imdbMovieUrl");
            }

            if (imageWidth < 30 || imageWidth > 300)
            {
                throw new ArgumentOutOfRangeException("imageWidth", imageWidth, "The image width must be within the [30-300] range.");
            }

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                return ParsePosterUrl(webClient.DownloadData(this.GetApiUrl(imdbMovieUrl)));
            }
        }

        public Uri GetApiUrl(Uri imdbUrl)
        {
            if (imdbUrl == null)
            {
                throw new ArgumentNullException("imdbUrl");
            }

            if (!imdbUrl.IsImdbMovieUrl())
            {
                throw new ArgumentException("The URL is not a valid IMDb movie URL.", "imdbUrl");
            }

            return new Uri(string.Format(ApiUrl, imdbUrl.GetImdbId(), this.apiKey, this.CalculateSecret(imdbUrl), 300));
        }

        public string CalculateSecret(Uri imdbUrl)
        {
            if (imdbUrl == null)
            {
                throw new ArgumentNullException("imdbUrl");
            }

            if (!imdbUrl.IsImdbMovieUrl())
            {
                throw new ArgumentException("The URL is not a valid IMDb movie URL.", "imdbUrl");
            }

            return (this.apiSecret + imdbUrl.GetImdbId()).ToMd5().Substring(10, 12);
        }

        private static Uri ParsePosterUrl(byte[] downloadString)
        {
            using (var stream = new MemoryStream(downloadString))
            {
                var serializer = new DataContractJsonSerializer(typeof(MoviePosterDbResult));
                var moviePosterDbResult = serializer.ReadObject(stream) as MoviePosterDbResult;

                if (moviePosterDbResult == null || moviePosterDbResult.Posters == null || moviePosterDbResult.Posters.Length == 0)
                {
                    return null;
                }

                return new Uri(moviePosterDbResult.Posters[0].ImageLocation);
            }
            
        }

        [DataContract]
        private class MoviePosterDbResult
        {
            [DataMember(Name = "posters")]
            public MoviePosterDbPoster[] Posters { get; set; }
        }

        [DataContract]
        private class MoviePosterDbPoster
        {
            [DataMember(Name = "image_location")]
            public string ImageLocation { get; set; }
        }
    }
}