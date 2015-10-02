namespace MoviePosterDb.Tests
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public class CheckTests
    {
        [Fact]
        public void NotNullOnNonNullParameterDoesNotThrowException()
        {
            // Arrange
            var nonNullObject = "not null";

            // Act

            // Assert
            Check.NotNull(nonNullObject, "nullObject");
        }

        [Fact]
        public void NotNullOnParameterIsNullThrowsArgumentNullException()
        {
            // Arrange
            string nullObject = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => Check.NotNull(nullObject, "nullObject"));
        }

        [Fact]
        public void NotNullOnParameterIsNullThrowsArgumentNullExceptionUsingSpecifiedParameterName()
        {
            // Arrange
            string nullObject = null;

            // Act

            // Assert
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => Check.NotNull(nullObject, "nullObject"));
            Assert.Equal("nullObject", argumentNullException.ParamName);
        }

        [Fact]
        public void NotNullOrEmptyOnNotNullOrEmptyOrEmptyParameterDoesNotThrowException()
        {
            // Arrange
            var nonNullObject = "not null";

            // Act

            // Assert
            Check.NotNullOrEmpty(nonNullObject, "nullObject");
        }

        [Fact]
        public void NotNullOrEmptyOnParameterIsNullThrowsArgumentNullException()
        {
            // Arrange
            string nullObject = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty(nullObject, "nullObject"));
        }

        [Fact]
        public void NotNullOrEmptyOnParameterIsNullThrowsArgumentNullExceptionUsingSpecifiedParameterName()
        {
            // Arrange
            string nullObject = null;

            // Act

            // Assert
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty(nullObject, "nullObject"));
            Assert.Equal("nullObject", argumentNullException.ParamName);
        }

        [Fact]
        public void NotNullOrEmptyOnParameterIsEmptyThrowsArgumentException()
        {
            // Arrange
            var nullObject = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => Check.NotNullOrEmpty(nullObject, "nullObject"));
        }

        [Fact]
        public void NotNullOrEmptyOnParameterIsEmptyThrowsArgumentExceptionUsingSpecifiedParameterName()
        {
            // Arrange
            var nullObject = string.Empty;

            // Act

            // Assert
            var argumentNullException = Assert.Throws<ArgumentException>(() => Check.NotNullOrEmpty(nullObject, "nullObject"));
            Assert.Equal("nullObject", argumentNullException.ParamName);
        }

        [Fact]
        public void ThatWithConditionIsTrueDoesNotThrowException()
        {
            // Arrange
            var nonNullObject = "not null";

            // Act

            // Assert
            Check.That("not null".Length > 0, nonNullObject, "Invalid length");
        }

        [Fact]
        public void ThatWithConditionIsFalseThrowsArgumentException()
        {
            // Arrange
            var nonNullObject = "not null";

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => Check.That("not null".Length == 0, nonNullObject, "Invalid length"));
        }

        [Fact]
        public void ThatWithConditionIsFalseThrowsArgumentExceptionUsingSpecifiedParameterName()
        {
            // Arrange
            var nonNullObject = "not null";

            // Act

            // Assert
            var argumentNullException = Assert.Throws<ArgumentException>(() => Check.That("not null".Length == 0, nonNullObject, "Invalid length"));
            Assert.Equal("not null", argumentNullException.ParamName);
        }

        [Fact]
        public void ThatWithConditionIsFalseThrowsArgumentExceptionUsingSpecifiedMessage()
        {
            // Arrange
            var nonNullObject = "not null";

            // Act

            // Assert
            var argumentNullException = Assert.Throws<ArgumentException>(() => Check.That("not null".Length == 0, nonNullObject, "Invalid length"));
            Assert.Contains("Invalid length", argumentNullException.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void GreaterThanZeroWithParameterGreaterThanZeroDoesNotThrowException(int validValue)
        {
            // Arrange

            // Act

            // Assert
            Check.GreaterThanZero(validValue, "validValue");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public void GreaterThanZeroWithParameterLessThanOneThrowsArgumentOutOfRangeException(int invalidValue)
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Check.GreaterThanZero(invalidValue, "invalidValue"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public void GreaterThanZeroWithParameterLessThanOneThrowsArgumentOutOfRangeExceptionUsingSpecifiedParameterName(int invalidValue)
        {
            // Arrange

            // Act

            // Assert
            var argumentOutOfRangeException = Assert.Throws<ArgumentOutOfRangeException>(() => Check.GreaterThanZero(invalidValue, "invalidValue"));
            Assert.Equal("invalidValue", argumentOutOfRangeException.ParamName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void InRangeWithParameterInRangeDoesNotThrowException(int validValue)
        {
            // Arrange

            // Act

            // Assert
            Check.InRange(validValue, 1, 100, "validValue");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        [InlineData(101)]
        [InlineData(200)]
        public void InRangeWithParameterLessThanOneThrowsArgumentOutOfRangeException(int invalidValue)
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Check.InRange(invalidValue, 1, 100,  "invalidValue"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        [InlineData(101)]
        [InlineData(200)]
        public void InRangeWithParameterLessThanOneThrowsArgumentOutOfRangeExceptionUsingSpecifiedParameterName(int invalidValue)
        {
            // Arrange

            // Act

            // Assert
            var argumentOutOfRangeException = Assert.Throws<ArgumentOutOfRangeException>(() => Check.InRange(invalidValue, 1, 100, "invalidValue"));
            Assert.Equal("invalidValue", argumentOutOfRangeException.ParamName);
        }
    }
}