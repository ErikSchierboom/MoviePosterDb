namespace MoviePosterDb.IntegrationTests
{
    using System;
    using System.Configuration;
    using System.Linq;

    using Xunit;
    using Xunit.Extensions;

    public class MoviePosterDbServiceTests
    {
        private const int ImageWidth = 100;
        private const int ImdbMovieIdWithPoster = 1375666;
        private const int ImdbMovieIdWithoutPoster = 196508;

        private static readonly string ApiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static readonly string ApiSecret = ConfigurationManager.AppSettings["ApiSecret"];
        
        [Fact]
        public void SearchUsingImdbMovieIdForMovieWithPosterWillReturnCorrectMoviePosterDbResult()
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(ImdbMovieIdWithPoster);

            // Assert
            Assert.Equal("Inception", moviePosterDbResult.Title);
            Assert.Equal("2010", moviePosterDbResult.Year);
            Assert.Equal("1375666", moviePosterDbResult.ImdbMovieId);
            Assert.Equal(@"http://api.movieposterdb.com/cache/normal/66/1375666/1375666_300.jpg", moviePosterDbResult.Posters[0].Url);
            Assert.Equal(1, moviePosterDbResult.Posters.Count());
        }

        [Fact]
        public void SearchUsingImdbMovieIdForMovieWithoutPosterReturnsNullForProperties()
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(ImdbMovieIdWithoutPoster);

            // Assert
            Assert.Null(moviePosterDbResult.Title);
            Assert.Null(moviePosterDbResult.Year);
            Assert.Null(moviePosterDbResult.ImdbMovieId);
            Assert.Null(moviePosterDbResult.Page);
            Assert.Null(moviePosterDbResult.Posters);
        }

        [Fact]
        public void SearchUsingImdbMovieIdAndImageWidthForMovieWithPosterWillReturnCorrectMoviePosterDbResult()
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(ImdbMovieIdWithPoster, ImageWidth);

            // Assert
            Assert.Equal("Inception", moviePosterDbResult.Title);
            Assert.Equal("2010", moviePosterDbResult.Year);
            Assert.Equal("1375666", moviePosterDbResult.ImdbMovieId);
            Assert.Equal(@"http://api.movieposterdb.com/cache/normal/66/1375666/1375666_100.jpg", moviePosterDbResult.Posters[0].Url);
            Assert.Equal(1, moviePosterDbResult.Posters.Count());
        }

        [Fact]
        public void SearchUsingImdbMovieIdAndImageWidthForMovieWithoutPosterReturnsNullForProperties()
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(ImdbMovieIdWithoutPoster, ImageWidth);

            // Assert
            Assert.Null(moviePosterDbResult.Title);
            Assert.Null(moviePosterDbResult.Year);
            Assert.Null(moviePosterDbResult.ImdbMovieId);
            Assert.Null(moviePosterDbResult.Page);
            Assert.Null(moviePosterDbResult.Posters);
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt1375666")]
        [InlineData("http://www.imdb.com/title/tt1375666/")]
        [InlineData("http://www.imdb.com/title/tt1375666/reference")]
        [InlineData("http://www.imdb.com/title/tt1375666/reference/")]
        public void SearchUsingImdbMovieUrlForMovieWithPosterWillReturnCorrectMoviePosterDbResult(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(new Uri(imdbMovieUrl));

            // Assert
            Assert.Equal("Inception", moviePosterDbResult.Title);
            Assert.Equal("2010", moviePosterDbResult.Year);
            Assert.Equal("1375666", moviePosterDbResult.ImdbMovieId);
            Assert.Equal(@"http://api.movieposterdb.com/cache/normal/66/1375666/1375666_300.jpg", moviePosterDbResult.Posters[0].Url);
            Assert.Equal(1, moviePosterDbResult.Posters.Count());
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt0196508")]
        [InlineData("http://www.imdb.com/title/tt0196508/")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference/")]
        public void SearchUsingImdbMovieUrlForMovieWithoutPosterReturnsNullForProperties(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(new Uri(imdbMovieUrl));

            // Assert
            Assert.Null(moviePosterDbResult.Title);
            Assert.Null(moviePosterDbResult.Year);
            Assert.Null(moviePosterDbResult.ImdbMovieId);
            Assert.Null(moviePosterDbResult.Page);
            Assert.Null(moviePosterDbResult.Posters);
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt1375666")]
        [InlineData("http://www.imdb.com/title/tt1375666/")]
        [InlineData("http://www.imdb.com/title/tt1375666/reference")]
        [InlineData("http://www.imdb.com/title/tt1375666/reference/")]
        public void SearchUsingImdbMovieUrlAndImageWidthForMovieWithPosterWillReturnCorrectMoviePosterDbResult(string imdbMovieUrl)
        {
            // Arrange
            var moviePosterDbService = new MoviePosterDbService(ApiKey, ApiSecret);

            // Act
            var moviePosterDbResult = moviePosterDbService.Search(new Uri(imdbMovieUrl), ImageWidth);

            // Assert
            Assert.Equal("Inception", moviePosterDbResult.Title);
            Assert.Equal("2010", moviePosterDbResult.Year);
            Assert.Equal("1375666", moviePosterDbResult.ImdbMovieId);
            Assert.Equal(@"http://api.movieposterdb.com/cache/normal/66/1375666/1375666_100.jpg", moviePosterDbResult.Posters[0].Url);
            Assert.Equal(1, moviePosterDbResult.Posters.Count());
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt0196508")]
        [InlineData("http://www.imdb.com/title/tt0196508/")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference")]
        [InlineData("http://www.imdb.com/title/tt0196508/reference/")]
        public void SearchUsingImdbMovieUrlAndImageWidthForMovieWithoutPosterReturnsNullForProperties(string imdbMovieUrl)
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