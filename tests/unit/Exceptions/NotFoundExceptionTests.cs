using System;
using OpenEchoSystem.GuardClauses.Exceptions;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.Exceptions
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            // Arrange
            var message = "Test message";

            // Act
            var exception = new NotFoundException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsMessageAndInnerException()
        {
            // Arrange
            var message = "Test message";
            var innerException = new Exception("Inner exception");

            // Act
            var exception = new NotFoundException(message, innerException);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}