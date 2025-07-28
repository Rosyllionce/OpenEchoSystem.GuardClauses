using System;
using System.Collections.Generic;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.ObjectGuards
{
    public class ObjectGuardClauseTests
    {
        [Fact]
        public void Null_WithNullObject_ThrowsArgumentNullException()
        {
            // Arrange
            object input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(input, nameof(input)));
        }

        [Fact]
        public void Null_WithNonNullObject_DoesNotThrow()
        {
            // Arrange
            var input = new object();

            // Act
            Guard.Against.Null(input, nameof(input));

            // Assert
            // No exception was thrown
        }
        [Fact]
        public void NotFound_WithNullObject_ThrowsArgumentException()
        {
            // Arrange
            object input = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.NotFound(input, nameof(input)));
        }

        [Fact]
        public void NotFound_WithNonNullObject_DoesNotThrow()
        {
            // Arrange
            var input = new object();

            // Act
            Guard.Against.NotFound(input, nameof(input));

            // Assert
            // No exception was thrown
        }
        [Fact]
        public void NullOrEmpty_WithNullEnumerable_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<string> input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrEmpty(input, nameof(input)));
        }

        [Fact]
        public void NullOrEmpty_WithEmptyEnumerable_ThrowsArgumentException()
        {
            // Arrange
            var input = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(input, nameof(input)));
        }

        [Fact]
        public void NullOrEmpty_WithNonEmptyEnumerable_DoesNotThrow()
        {
            // Arrange
            var input = new List<string> { "test" };

            // Act
            Guard.Against.NullOrEmpty(input, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        public void OutOfRange_WithDateBeforeMinimum_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var input = new DateTime(2023, 1, 1);
            var minimum = new DateTime(2023, 1, 2);
            var maximum = new DateTime(2023, 1, 3);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, minimum, maximum, nameof(input)));
        }

        [Fact]
        public void OutOfRange_WithDateAfterMaximum_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var input = new DateTime(2023, 1, 4);
            var minimum = new DateTime(2023, 1, 2);
            var maximum = new DateTime(2023, 1, 3);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(input, minimum, maximum, nameof(input)));
        }

        [Fact]
        public void OutOfRange_WithDateEqualToMinimum_DoesNotThrow()
        {
            // Arrange
            var input = new DateTime(2023, 1, 2);
            var minimum = new DateTime(2023, 1, 2);
            var maximum = new DateTime(2023, 1, 3);

            // Act
            Guard.Against.OutOfRange(input, minimum, maximum, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        public void OutOfRange_WithDateEqualToMaximum_DoesNotThrow()
        {
            // Arrange
            var input = new DateTime(2023, 1, 3);
            var minimum = new DateTime(2023, 1, 2);
            var maximum = new DateTime(2023, 1, 3);

            // Act
            Guard.Against.OutOfRange(input, minimum, maximum, nameof(input));

            // Assert
            // No exception was thrown
        }

        [Fact]
        public void OutOfRange_WithDateInRange_DoesNotThrow()
        {
            // Arrange
            var input = new DateTime(2023, 1, 2);
            var minimum = new DateTime(2023, 1, 1);
            var maximum = new DateTime(2023, 1, 3);

            // Act
            Guard.Against.OutOfRange(input, minimum, maximum, nameof(input));

            // Assert
            // No exception was thrown
        }
    }
}