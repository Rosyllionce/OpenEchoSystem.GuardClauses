using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.UnitTests.CustomFoundationalGuards
{
    public class EnumGuardTests
    {
        private enum TestEnum { A, B }

        [Fact]
        public void OutOfRange_UndefinedValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var paramName = "testParam";
            var invalidEnumValue = (TestEnum)99;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(invalidEnumValue, paramName));
        }

        [Fact]
        public void OutOfRange_DefinedValue_DoesNotThrow()
        {
            // Arrange
            var paramName = "testParam";
            var validEnumValue = TestEnum.A;

            // Act
            Guard.Against.OutOfRange(validEnumValue, paramName);

            // Assert
            // No exception thrown is a pass.
        }

        [Fact]
        public void OutOfRange_UndefinedValue_ExceptionMessageContainsParamName()
        {
            // Arrange
            var paramName = "myEnumParam";
            var invalidEnumValue = (TestEnum)99;

            // Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(invalidEnumValue, paramName));

            // Assert
            Assert.Contains(paramName, exception.Message);
        }
    }
}