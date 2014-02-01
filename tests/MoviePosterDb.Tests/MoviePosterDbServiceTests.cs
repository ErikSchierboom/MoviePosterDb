namespace MoviePosterDb.Tests
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public class MoviePosterDbServiceTests
    {
        private const string ApiKey = "test-api-key";
        private const string ApiSecret = "test-api-secret";
        private const string ImdbMovieUrl = "http://www.imdb.com/title/tt1234567/";
        private const int ImageWidth = 300;

        [Fact]
        public void ConstructorWithNullApiKeyThrowsArgumentNullException()
        {
            // Arrange
            string nullApiKey = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new MoviePosterDbService(nullApiKey, "api secret"));
        }

        [Fact]
        public void ConstructorWithEmptyApiKeyThrowsArgumentNullException()
        {
            // Arrange
            var emptyApiKey = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new MoviePosterDbService(emptyApiKey, "api secret"));
        }

        [Fact]
        public void ConstructorWithNullApiSecretThrowsArgumentNullException()
        {
            // Arrange
            string nullApiSecret = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new MoviePosterDbService("api key", nullApiSecret));
        }

        [Fact]
        public void ConstructorWithEmptyApiSecretThrowsArgumentNullException()
        {
            // Arrange
            var emptyApiSecret = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new MoviePosterDbService("api key", emptyApiSecret));
        }

        [Fact]
        public void GetPosterUrlWithNullImdbMovieUrlThrowsArgumentNullException()
        {
            // Arrange
            string nullImdbMovieUrl = null;
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => moviePosterDbService.GetPosterUrl(nullImdbMovieUrl, ImageWidth));
        }

        [Theory]
        [InlineData("http://www.imdb.com/")]
        [InlineData("http://www.imdb.com/list/PQDCzc8WwVQ/")]
        [InlineData("http://www.imdb.com/search/title?genres=drama&title_type=feature&num_votes=5000,&sort=user_rating,desc")]
        [InlineData("http://www.imdb.com/search/title?release_date=1990,1999&title_type=feature&num_votes=5000,&sort=user_rating,desc")]
        [InlineData("http://www.imdb.com/chart/top/")]
        [InlineData("http://www.imdb.com/boxoffice/alltimegross?region=world-wide")]
        [InlineData("http://www.imdb.com/user/ur3342822/ratings")]
        [InlineData("http://www.google.com")]
        public void GetPosterUrlWithInvalidImdbMovieUrlThrowsArgumentException(string invalidImdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => moviePosterDbService.GetPosterUrl(invalidImdbMovieUrl, ImageWidth));
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt123456/")]
        [InlineData("http://www.imdb.com/title/tt1/")]
        [InlineData("http://www.imdb.com/title/tt/")]
        [InlineData("http://www.imdb.com/title/")]
        [InlineData("http://www.imdb.com/title")]
        public void GetPosterUrlWithIncompleteImdbMovieUrlThrowsArgumentException(string incompleteImdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => moviePosterDbService.GetPosterUrl(incompleteImdbMovieUrl, ImageWidth));
        }

        [Fact]
        public void GetPosterUrlOverloadWithNullImdbMovieUrlThrowsArgumentNullException()
        {
            // Arrange
            Uri nullImdbMovieUrl = null;
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => moviePosterDbService.GetPosterUrl(nullImdbMovieUrl, ImageWidth));
        }

        [Theory]
        [InlineData("http://www.imdb.com/")]
        [InlineData("http://www.imdb.com/list/PQDCzc8WwVQ/")]
        [InlineData("http://www.imdb.com/search/title?genres=drama&title_type=feature&num_votes=5000,&sort=user_rating,desc")]
        [InlineData("http://www.imdb.com/search/title?release_date=1990,1999&title_type=feature&num_votes=5000,&sort=user_rating,desc")]
        [InlineData("http://www.imdb.com/chart/top/")]
        [InlineData("http://www.imdb.com/boxoffice/alltimegross?region=world-wide")]
        [InlineData("http://www.imdb.com/user/ur3342822/ratings")]
        [InlineData("http://www.google.com")]
        public void GetPosterUrlOverloadWithInvalidImdbMovieUrlThrowsArgumentException(string invalidImdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => moviePosterDbService.GetPosterUrl(new Uri(invalidImdbMovieUrl), ImageWidth));
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt123456/")]
        [InlineData("http://www.imdb.com/title/tt1/")]
        [InlineData("http://www.imdb.com/title/tt/")]
        [InlineData("http://www.imdb.com/title/")]
        [InlineData("http://www.imdb.com/title")]
        public void GetPosterUrlOverloadWithIncompleteImdbMovieUrlThrowsArgumentException(string incompleteImdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => moviePosterDbService.GetPosterUrl(new Uri(incompleteImdbMovieUrl), ImageWidth));
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        [InlineData(29)]
        [InlineData(301)]
        [InlineData(500)]
        public void GetPosterUrlOverloadWithImageWidthOutOfRangeThrowsArgumentOutOfRangeException(int invalidImageWidth)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => moviePosterDbService.GetPosterUrl(new Uri(ImdbMovieUrl), invalidImageWidth));
        }
        [Theory]
        [InlineData("http://www.imdb.com/title/tt2304771")]
        [InlineData("http://www.imdb.com/title/tt2304771/")]
        [InlineData("http://www.imdb.com/title/tt2304771/reference")]
        [InlineData("http://www.imdb.com/title/tt2304771/reference/")]
        public void GetApiUrlReturnsCorrectApiUrl(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var apiUrl = moviePosterDbService.GetApiUrl(new Uri(imdbMovieUrl));

            // Assert
            Assert.Equal("http://api.movieposterdb.com/json?imdb_code=2304771&api_key=test-api-key&secret=8435ce4c53ff&width=300", apiUrl.ToString());
        }

        [Fact]
        public void GetApiUrlWithNullImdbMovieUrlThrowsArgumentNullException()
        {
            // Arrange
            Uri nullImdbMovieUrl = null;
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => moviePosterDbService.GetApiUrl(nullImdbMovieUrl));
        }

        [Theory]
        [InlineData("http://www.google.nl")]
        [InlineData("http://www.imdb.com")]
        [InlineData("http://www.imdb.com/chart/top")]
        [InlineData("http://www.imdb.com/list/PQDCzc8WwVQ/")]
        [InlineData("http://www.imdb.com/user/ur3342822/ratings")]
        public void GetApiUrlWithInvalidImdbMovieUrlThrowsArgumentException(string invalidImdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => moviePosterDbService.GetApiUrl(new Uri(invalidImdbMovieUrl)));
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt2304771")]
        [InlineData("http://www.imdb.com/title/tt2304771/")]
        [InlineData("http://www.imdb.com/title/tt2304771/reference")]
        [InlineData("http://www.imdb.com/title/tt2304771/reference/")]
        public void CalculateSecretReturnsCorrectSecret(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var calculateSecret = moviePosterDbService.CalculateSecret(new Uri(imdbMovieUrl));

            // Assert
            Assert.Equal("8435ce4c53ff", calculateSecret);
        }

        [Fact]
        public void CalculateSecretWithNullImdbMovieUrlThrowsArgumentNullException()
        {
            // Arrange
            Uri nullImdbMovieUrl = null;
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => moviePosterDbService.CalculateSecret(nullImdbMovieUrl));
        }

        [Theory]
        [InlineData("http://www.google.nl")]
        [InlineData("http://www.imdb.com")]
        [InlineData("http://www.imdb.com/chart/top")]
        [InlineData("http://www.imdb.com/list/PQDCzc8WwVQ/")]
        [InlineData("http://www.imdb.com/user/ur3342822/ratings")]
        public void CalculateSecretWithInvalidImdbUrlThrowsArgumentException(string invalidImdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => moviePosterDbService.CalculateSecret(new Uri(invalidImdbMovieUrl)));
        }
    }
}