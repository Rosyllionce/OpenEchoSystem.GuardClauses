using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.NullGuards
{
    public class GuardClauseNullExtensionsTests
    {
        private readonly IGuardClause _guard = new GuardClause();

        [Fact]
        public void Null_WhenInputIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            object input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _guard.Null(input, "param"));
        }

        [Fact]
        public void Null_WhenInputIsNotNull_DoesNotThrow()
        {
            // Arrange
            var input = new object();

            // Act
            _guard.Null(input, "param");

            // Assert
            // No exception is thrown
        }

        [Fact]
        public void Null_WhenInputIsNullAndCustomMessageIsProvided_ThrowsArgumentNullExceptionWithCustomMessage()
        {
            // Arrange
            object input = null;
            var message = "Custom message";

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _guard.Null(input, "param", message));
            Assert.Contains(message, ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NullOrEmpty_WhenInputIsNullOrEmpty_ThrowsException(string input)
        {
            // Act & Assert
            if (input is null)
            {
                Assert.Throws<ArgumentNullException>(() => _guard.NullOrEmpty(input, "param"));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => _guard.NullOrEmpty(input, "param"));
            }
        }

        [Fact]
        public void NullOrEmpty_WhenInputIsNotNullOrEmpty_DoesNotThrow()
        {
            // Arrange
            var input = "test";

            // Act
            _guard.NullOrEmpty(input, "param");

            // Assert
            // No exception is thrown
        }
        
        [Fact]
        public void NullOrEmpty_WhenInputIsEmptyAndCustomMessageIsProvided_ThrowsArgumentExceptionWithCustomMessage()
        {
            // Arrange
            string input = "";
            var message = "Custom message";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _guard.NullOrEmpty(input, "param", message));
            Assert.Contains(message, ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NullOrWhiteSpace_WhenInputIsNullOrWhiteSpace_ThrowsException(string input)
        {
            // Act & Assert
            if (input is null)
            {
                Assert.Throws<ArgumentNullException>(() => _guard.NullOrWhiteSpace(input, "param"));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => _guard.NullOrWhiteSpace(input, "param"));
            }
        }

        [Fact]
        public void NullOrWhiteSpace_WhenInputIsNotNullOrWhiteSpace_DoesNotThrow()
        {
            // Arrange
            var input = "test";

            // Act
            _guard.NullOrWhiteSpace(input, "param");

            // Assert
            // No exception is thrown
        }

        [Fact]
        public void NullOrWhiteSpace_WhenInputIsWhiteSpaceAndCustomMessageIsProvided_ThrowsArgumentExceptionWithCustomMessage()
        {
            // Arrange
            string input = " ";
            var message = "Custom message";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _guard.NullOrWhiteSpace(input, "param", message));
            Assert.Contains(message, ex.Message);
        }

        [Fact]
        public void Null_WhenInputIsNotNullAndCustomMessageIsProvided_DoesNotThrow()
        {
            // Arrange
            var input = new object();
            var message = "Custom message";

            // Act
            _guard.Null(input, "param", message);

            // Assert
            // No exception is thrown
        }

        [Fact]
        public void Null_WhenInputIsNullAndMessageIsNull_ThrowsArgumentNullExceptionWithDefaultMessage()
        {
            // Arrange
            object input = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _guard.Null(input, "param", null));
            Assert.Contains("Parameter [param] cannot be null.", ex.Message);
        }
    }
}