using System;
using System.Collections.Generic;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.ObjectGuards
{
    public class GuardClauseObjectExtensionsTests
    {
        private readonly IGuardClause _guard = new GuardClause();

        [Fact]
        public void Null_WhenInputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            object input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _guard.Null(input));
        }

        [Fact]
        public void Null_WhenInputIsNotNull_DoesNotThrow()
        {
            // Arrange
            var input = new object();

            // Act
            _guard.Null(input);

            // Assert
            // No exception is thrown
        }

        [Fact]
        public void NotFound_WhenInputIsNull_ThrowsArgumentException()
        {
            // Arrange
            object input = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _guard.NotFound(input));
        }

        [Fact]
        public void NotFound_WhenInputIsNotNull_DoesNotThrow()
        {
            // Arrange
            var input = new object();

            // Act
            _guard.NotFound(input);

            // Assert
            // No exception is thrown
        }

        [Fact]
        public void NullOrEmpty_WhenInputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            List<int> input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _guard.NullOrEmpty(input));
        }

        [Fact]
        public void NullOrEmpty_WhenInputIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var input = new List<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _guard.NullOrEmpty(input));
        }

        [Fact]
        public void NullOrEmpty_WhenInputIsNotNullOrEmpty_DoesNotThrow()
        {
            // Arrange
            var input = new List<int> { 1 };

            // Act
            _guard.NullOrEmpty(input);

            // Assert
            // No exception is thrown
        }

        [Theory]
        [InlineData("2023-01-01", "2023-01-02", "2023-01-03")]
        [InlineData("2023-01-04", "2023-01-02", "2023-01-03")]
        public void OutOfRange_WhenInputIsOutOfRange_ThrowsArgumentOutOfRangeException(string input, string min, string max)
        {
            // Arrange
            var date = DateTime.Parse(input);
            var minDate = DateTime.Parse(min);
            var maxDate = DateTime.Parse(max);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _guard.OutOfRange(date, minDate, maxDate));
        }

        [Fact]
        public void OutOfRange_WhenInputIsInRange_DoesNotThrow()
        {
            // Arrange
            var date = new DateTime(2023, 1, 2);
            var minDate = new DateTime(2023, 1, 1);
            var maxDate = new DateTime(2023, 1, 3);

            // Act
            _guard.OutOfRange(date, minDate, maxDate);

            // Assert
            // No exception is thrown
        }
    }
}