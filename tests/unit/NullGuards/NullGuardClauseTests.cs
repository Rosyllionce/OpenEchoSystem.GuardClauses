using System;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTest.NullGuards
{
    public class NullGuardClauseTests
    {
        // Test Cases for Guard.Against.Null<T>

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void Null_GivenNullObject_ThrowsArgumentNullException()
        {
            // Arrange
            object input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(input, nameof(input)));
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void Null_GivenNonNullObject_DoesNothing()
        {
            // Arrange
            var input = new object();

            // Act
            Guard.Against.Null(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void Null_GivenNonNullString_DoesNothing()
        {
            // Arrange
            var input = "hello";

            // Act
            Guard.Against.Null(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void Null_GivenEmptyString_DoesNothing()
        {
            // Arrange
            var input = "";

            // Act
            Guard.Against.Null(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        // Test Cases for Guard.Against.NullOrEmpty

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrEmpty_GivenNullString_ThrowsArgumentNullException()
        {
            // Arrange
            string input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrEmpty(input, nameof(input)));
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrEmpty_GivenEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var input = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(input, nameof(input)));
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrEmpty_GivenNonEmptyString_DoesNothing()
        {
            // Arrange
            var input = "hello";

            // Act
            Guard.Against.NullOrEmpty(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrEmpty_GivenWhitespaceString_DoesNothing()
        {
            // Arrange
            var input = " ";

            // Act
            Guard.Against.NullOrEmpty(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        // Test Cases for Guard.Against.NullOrWhiteSpace

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrWhiteSpace_GivenNullString_ThrowsArgumentNullException()
        {
            // Arrange
            string input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrWhiteSpace(input, nameof(input)));
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrWhiteSpace_GivenEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var input = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrWhiteSpace(input, nameof(input)));
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrWhiteSpace_GivenWhitespaceString_ThrowsArgumentException()
        {
            // Arrange
            var input = " ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrWhiteSpace(input, nameof(input)));
        }

        [Theory]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("\t\r\n")]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrWhiteSpace_GivenVariousWhitespaceStrings_ThrowsArgumentException(string input)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrWhiteSpace(input, nameof(input)));
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrWhiteSpace_GivenNonEmptyString_DoesNothing()
        {
            // Arrange
            var input = "hello";

            // Act
            Guard.Against.NullOrWhiteSpace(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        [Trait("Category", "CoreGuard")]
        [Trait("Feature", "NullGuards")]
        public void NullOrWhiteSpace_GivenStringWithLeadingAndTrailingWhitespace_DoesNothing()
        {
            // Arrange
            var input = "  hello  ";

            // Act
            Guard.Against.NullOrWhiteSpace(input, nameof(input));

            // Assert
            // No exception was thrown
        }
    }
}