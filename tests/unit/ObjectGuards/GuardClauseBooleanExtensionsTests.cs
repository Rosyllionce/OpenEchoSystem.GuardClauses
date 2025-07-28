using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.ObjectGuards
{
    public class GuardClauseBooleanExtensionsTests
    {
        [Fact]
        public void CustomCondition_WithTrueCondition_ThrowsArgumentException()
        {
            // Arrange
            var guard = new GuardClause();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => guard.CustomCondition(true));
        }

        [Fact]
        public void CustomCondition_WithFalseCondition_DoesNotThrow()
        {
            // Arrange
            var guard = new GuardClause();

            // Act
            guard.CustomCondition(false);

            // Assert
            // No exception thrown
        }

        [Fact]
        public void CustomCondition_WithTrueConditionAndCustomMessage_ThrowsArgumentExceptionWithCustomMessage()
        {
            // Arrange
            var guard = new GuardClause();
            var message = "Custom message";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => guard.CustomCondition(true, message: message));
            Assert.Contains(message, ex.Message);
        }
    }
}