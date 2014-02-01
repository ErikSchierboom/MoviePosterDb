namespace MoviePosterDb.IntegrationTests
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public class MoviePosterDbServiceTests
    {
        private const int ImageWidth = 300;

        private static string ApiKey
        {
            get
            {
                throw new NotImplementedException("The API key must be known before integration testing");
            }
        }

        private static string ApiSecret
        {
            get
            {
                throw new NotImplementedException("The API secret must be known before integration testing");
            }
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt2304771")]
        [InlineData("http://www.imdb.com/title/tt2304771/")]
        [InlineData("http://www.imdb.com/title/tt2304771/reference")]
        [InlineData("http://www.imdb.com/title/tt2304771/reference/")]
        public void GetPosterUrlForMovieWithPosterWillReturnCorrectPosterOfSpecifiedWidth(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var posterUrl = moviePosterDbService.GetPosterUrl(new Uri(imdbMovieUrl), ImageWidth);

            // Assert
            Assert.Equal(@"http://api.movieposterdb.com/cache/normal/71/2304771/2304771_300.jpg", posterUrl.ToString());
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt0196508")]
        [InlineData("http://www.imdb.com/title/tt0196508/")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference/")]
        public void GetPosterUrlForMovieWithoutPosterReturnsNull(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var posterUrl = moviePosterDbService.GetPosterUrl(new Uri(imdbMovieUrl), ImageWidth);

            // Assert
            Assert.Null(posterUrl);
        }
    }
}