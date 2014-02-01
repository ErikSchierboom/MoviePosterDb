namespace MoviePosterDb.Tests
{
    using System;

    using Xunit;

    public class CryptographyExtensionsTests
    {
        [Fact]
        public void ToMd5ReturnsCorrectHash()
        {
            // Arrange

            // Act
            var md5 = "foo".ToMd5();

            // Assert
            Assert.Equal("acbd18db4cc2f85cedef654fccc4a4d8", md5);
        }

        [Fact]
        public void ToMd5WithEmptyStringReturnsCorrectHash()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act
            var md5 = emptyString.ToMd5();

            // Assert
            Assert.Equal("d41d8cd98f00b204e9800998ecf8427e", md5);
        }

        [Fact]
        public void ToMd5WithNullStringThrowsArgumentNullException()
        {
            // Arrange
            string nullString = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => nullString.ToMd5());
        }
    }
}