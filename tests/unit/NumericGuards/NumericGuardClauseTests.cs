using System;
using FluentAssertions;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.NumericGuards
{
    public class NumericGuardClauseTests
    {
        // Test Case: NUM-OOR-001
        [Fact]
        [Trait("Category", "NegativePath")]
        public void OutOfRange_InputIsLessThanFrom_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int input = 4;
            int from = 5;
            int to = 10;
            Action action = () => Guard.Against.OutOfRange(input, from, to, nameof(input));

            // Act & Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        // Test Case: NUM-OOR-002
        [Fact]
        [Trait("Category", "NegativePath")]
        public void OutOfRange_InputIsGreaterThanTo_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int input = 11;
            int from = 5;
            int to = 10;
            Action action = () => Guard.Against.OutOfRange(input, from, to, nameof(input));

            // Act & Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        // Test Case: NUM-OOR-003
        [Fact]
        [Trait("Category", "CriticalPath")]
        public void OutOfRange_InputIsWithinRange_DoesNotThrowException()
        {
            // Arrange
            int input = 7;
            int from = 5;
            int to = 10;
            Action action = () => Guard.Against.OutOfRange(input, from, to, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }

        // Test Case: NUM-OOR-004
        [Fact]
        [Trait("Category", "EdgeCase")]
        public void OutOfRange_InputIsEqualToFrom_DoesNotThrowException()
        {
            // Arrange
            int input = 5;
            int from = 5;
            int to = 10;
            Action action = () => Guard.Against.OutOfRange(input, from, to, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }

        // Test Case: NUM-OOR-005
        [Fact]
        [Trait("Category", "EdgeCase")]
        public void OutOfRange_InputIsEqualToTo_DoesNotThrowException()
        {
            // Arrange
            int input = 10;
            int from = 5;
            int to = 10;
            Action action = () => Guard.Against.OutOfRange(input, from, to, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }

        // Test Case: NUM-ZERO-001
        [Fact]
        [Trait("Category", "NegativePath")]
        public void Zero_InputIsZero_ThrowsArgumentException()
        {
            // Arrange
            int input = 0;
            Action action = () => Guard.Against.Zero(input, nameof(input));

            // Act & Assert
            action.Should().Throw<ArgumentException>();
        }

        // Test Case: NUM-ZERO-002
        [Fact]
        [Trait("Category", "CriticalPath")]
        public void Zero_InputIsPositive_DoesNotThrowException()
        {
            // Arrange
            int input = 1;
            Action action = () => Guard.Against.Zero(input, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }

        // Test Case: NUM-ZERO-003
        [Fact]
        [Trait("Category", "CriticalPath")]
        public void Zero_InputIsNegative_DoesNotThrowException()
        {
            // Arrange
            int input = -1;
            Action action = () => Guard.Against.Zero(input, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }

        // Test Case: NUM-NEG-001
        [Fact]
        [Trait("Category", "NegativePath")]
        public void Negative_InputIsNegative_ThrowsArgumentException()
        {
            // Arrange
            int input = -1;
            Action action = () => Guard.Against.Negative(input, nameof(input));

            // Act & Assert
            action.Should().Throw<ArgumentException>();
        }

        // Test Case: NUM-NEG-002
        [Fact]
        [Trait("Category", "EdgeCase")]
        public void Negative_InputIsZero_DoesNotThrowException()
        {
            // Arrange
            int input = 0;
            Action action = () => Guard.Against.Negative(input, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }

        // Test Case: NUM-NEG-003
        [Fact]
        [Trait("Category", "CriticalPath")]
        public void Negative_InputIsPositive_DoesNotThrowException()
        {
            // Arrange
            int input = 1;
            Action action = () => Guard.Against.Negative(input, nameof(input));

            // Act & Assert
            action.Should().NotThrow();
        }
    }
}