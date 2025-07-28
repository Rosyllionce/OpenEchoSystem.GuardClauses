using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.BooleanGuards
{
    public class BooleanGuardTests
    {
        [Fact]
        public void CustomCondition_TrueCondition_ThrowsArgumentException()
        {
            // Arrange
            var condition = true;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.CustomCondition(condition));
        }

        [Fact]
        public void CustomCondition_FalseCondition_DoesNotThrow()
        {
            // Arrange
            var condition = false;

            // Act
            Guard.Against.CustomCondition(condition);

            // Assert
            // No exception was thrown
        }

        [Fact]
        public void CustomCondition_TrueConditionWithCustomMessage_ThrowsAndContainsCustomMessage()
        {
            // Arrange
            var condition = true;
            var message = "Custom message";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => Guard.Against.CustomCondition(condition, message: message));

            // Assert
            Assert.Contains(message, ex.Message);
        }
    }
}