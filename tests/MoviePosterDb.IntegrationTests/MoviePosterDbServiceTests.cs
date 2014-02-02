namespace MoviePosterDb.IntegrationTests
{
    using System;
    using System.Linq;

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
        public void SearchWithImdbMovieIdForMovieWithPosterWillReturnCorrectMoviePosterDbResult(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(new Uri(imdbMovieUrl), ImageWidth);

            // Assert
            Assert.Equal(@"Mandela: Long Walk to Freedom", moviePosterDbResult.Title);
            Assert.Equal("2013", moviePosterDbResult.Year);
            Assert.Equal("2304771", moviePosterDbResult.ImdbMovieId);
            Assert.Equal(@"http://api.movieposterdb.com/cache/normal/71/2304771/2304771_300.jpg", moviePosterDbResult.Posters[0].ImageLocation);
            Assert.Equal(1, moviePosterDbResult.Posters.Count());
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt0196508")]
        [InlineData("http://www.imdb.com/title/tt0196508/")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference/")]
        public void SearchWithImdbMovieIdForMovieWithoutPosterReturnsNullForProperties(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(new Uri(imdbMovieUrl), ImageWidth);

            // Assert
            Assert.Null(moviePosterDbResult.Title);
            Assert.Null(moviePosterDbResult.Year);
            Assert.Null(moviePosterDbResult.ImdbMovieId);
            Assert.Null(moviePosterDbResult.Page);
            Assert.Null(moviePosterDbResult.Posters);
        }
    }
}