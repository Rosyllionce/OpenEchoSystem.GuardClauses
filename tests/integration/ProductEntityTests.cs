using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    public class ProductEntityTests
    {
        [Fact]
        public void ValidateProductEntityWithValidInputShouldNotThrow()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var validName = "Valid Product";
            var validPrice = 10.0m;
            var validSupplierId = Guid.NewGuid();

            // Act & Assert
            var exception = Record.Exception(() => new Product(validId, validName, validPrice, validSupplierId));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateProductEntityWithInvalidNameNullShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Product(Guid.NewGuid(), null!, 10.0m, Guid.NewGuid()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateProductEntityWithInvalidNameWhitespaceShouldThrow(string name)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(Guid.NewGuid(), name, 10.0m, Guid.NewGuid()));
        }

        [Fact]
        public void ValidateProductEntityWithZeroPriceShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(Guid.NewGuid(), "Valid Product", 0, Guid.NewGuid()));
        }

        [Fact]
        public void ValidateProductEntityWithNegativePriceShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(Guid.NewGuid(), "Valid Product", -1, Guid.NewGuid()));
        }

        [Fact]
        public void ValidateProductEntityWithEmptySupplierIdShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(Guid.NewGuid(), "Valid Product", 10.0m, Guid.Empty));
        }
    }
}