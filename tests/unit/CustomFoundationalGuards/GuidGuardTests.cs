using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.CustomFoundationalGuards
{
    public class GuidGuardTests
    {
        [Fact]
        public void Empty_EmptyGuid_ThrowsArgumentException()
        {
            // Arrange
            var paramName = "testParam";
            var emptyGuid = Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.Empty(emptyGuid, paramName));
        }

        [Fact]
        public void Empty_NonEmptyGuid_DoesNotThrow()
        {
            // Arrange
            var paramName = "testParam";
            var nonEmptyGuid = Guid.NewGuid();

            // Act
            Guard.Against.Empty(nonEmptyGuid, paramName);

            // Assert
            // No exception thrown is a pass.
        }

        [Fact]
        public void Empty_EmptyGuid_ExceptionMessageContainsParamName()
        {
            // Arrange
            var paramName = "myGuidParam";
            var emptyGuid = Guid.Empty;

            // Act
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.Empty(emptyGuid, paramName));

            // Assert
            Assert.Contains(paramName, exception.Message);
        }
    }
}