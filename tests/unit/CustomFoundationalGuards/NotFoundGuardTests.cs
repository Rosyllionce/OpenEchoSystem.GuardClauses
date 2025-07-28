using System;
using OpenEchoSystem.GuardClauses;
using OpenEchoSystem.GuardClauses.Exceptions;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.CustomFoundationalGuards
{
    public class NotFoundGuardTests
    {
        private class User { }
        private class Product { }

        [Fact]
        public void NotFound_NullLookup_ThrowsNotFoundException()
        {
            // Arrange
            var userId = "user-123";
            Func<string, User> lookup = (key) => null;

            // Act & Assert
            Assert.Throws<NotFoundException>(() => Guard.Against.NotFound(userId, lookup));
        }

        [Fact]
        public void NotFound_ValidLookup_DoesNotThrow()
        {
            // Arrange
            var userId = "user-123";
            Func<string, User> lookup = (key) => new User();

            // Act
            Guard.Against.NotFound(userId, lookup);

            // Assert
            // No exception thrown is a pass.
        }

        [Fact]
        public void NotFound_NullLookup_ExceptionMessageContainsKey()
        {
            // Arrange
            var productId = "prod-abc";
            Func<string, Product> lookup = (key) => null;

            // Act
            var exception = Assert.Throws<NotFoundException>(() => Guard.Against.NotFound(productId, lookup));

            // Assert
            Assert.Contains(productId, exception.Message);
        }
    }
}