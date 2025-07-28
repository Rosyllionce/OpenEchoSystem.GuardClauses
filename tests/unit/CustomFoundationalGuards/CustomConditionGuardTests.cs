using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.CustomFoundationalGuards
{
    public class CustomConditionGuardTests
    {
        [Fact]
        public void CustomCondition_TrueCondition_ThrowsArgumentException()
        {
            // Arrange
            var paramName = "testParam";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.CustomCondition(true, paramName));
        }

        [Fact]
        public void CustomCondition_FalseCondition_DoesNotThrow()
        {
            // Arrange
            var paramName = "testParam";

            // Act
            Guard.Against.CustomCondition(false, paramName);

            // Assert
            // No exception thrown is a pass.
        }

        [Fact]
        public void CustomCondition_TrueCondition_ExceptionMessageContainsParamName()
        {
            // Arrange
            var paramName = "myCustomParam";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.CustomCondition(true, paramName));

            // Assert
            Assert.Contains(paramName, exception.Message);
        }
    }
}