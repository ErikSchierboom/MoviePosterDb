namespace MoviePosterDb.Tests
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public class UriExtensionsTests
    {
        [Theory]
        [InlineData("http://www.imdb.com/title/tt1234567", 1234567)]
        [InlineData("http://www.imdb.com/title/tt0234567", 234567)]
        [InlineData("http://www.imdb.com/title/tt1234567/", 1234567)]
        [InlineData("http://www.imdb.com/title/tt0234567/", 234567)]
        [InlineData("http://www.imdb.com/title/tt1234567/reference", 1234567)]
        [InlineData("http://www.imdb.com/title/tt0234567/reference", 234567)]
        public void GetImdbIdWithValidImdbMovieUrlReturnsImdbIdFromUrl(string validImdbMovieUrl, int expectedImdbId)
        {
            // Arrange
            var url = new Uri(validImdbMovieUrl);

            // Act
            var imdbId = url.GetImdbId();

            // Assert
            Assert.Equal(expectedImdbId, imdbId);
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
        public void ParseWithNonMovieUrlThrowsArgumentException(string invalidImdbMovieUrl)
        {
            // Arrange
            var url = new Uri(invalidImdbMovieUrl);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => url.GetImdbId());
        }

        [Theory]
        [InlineData("http://www.imdb.com/title/tt123456/")]
        [InlineData("http://www.imdb.com/title/tt1/")]
        [InlineData("http://www.imdb.com/title/tt/")]
        [InlineData("http://www.imdb.com/title/")]
        [InlineData("http://www.imdb.com/title")]
        public void GetImdbIdWithIncompleteImdbMovieUrlThrowsArgumentException(string incompleteImdbMovieUrl)
        {
            // Arrange
            var url = new Uri(incompleteImdbMovieUrl);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => url.GetImdbId());
        }

        [Fact]
        public void GetImdbIdOnNullUrlThrowsArgumentNullException()
        {
            // Arrange
            Uri nullUrl = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => nullUrl.GetImdbId());
        }
    }
}