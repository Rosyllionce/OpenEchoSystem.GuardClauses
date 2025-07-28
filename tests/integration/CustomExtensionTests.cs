using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    // 1. Define a custom guard clause extension
    public static class CustomGuardExtensions
    {
        public static void InvalidState(this IGuardClause guardClause, OrderStatus status, string parameterName)
        {
            if (status == OrderStatus.Shipped || status == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException($"Order with status '{status}' cannot be modified.");
            }
        }
    }

    // 2. Create a mock service that uses the custom guard
    public class OrderModificationService
    {
        public static void AddProductToOrder(Order order, Product product)
        {
            Guard.Against.Null(order, nameof(order));
            Guard.Against.Null(product, nameof(product));

            // Use the custom guard to prevent modification of shipped/cancelled orders
            Guard.Against.InvalidState(order.Status, nameof(order.Status));

            // ... logic to add product to order
        }
    }

    // 3. Write the integration test to verify the custom guard
    public class CustomExtensionTests
    {
        [Fact]
        public void AddProductToOrderWhenOrderIsShippedThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order { Id = Guid.NewGuid(), Status = OrderStatus.Shipped };
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Price = 10m };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => OrderModificationService.AddProductToOrder(order, product));
            Assert.Equal("Order with status 'Shipped' cannot be modified.", exception.Message);
        }

        [Fact]
        public void AddProductToOrderWhenOrderIsProcessingShouldNotThrow()
        {
            // Arrange
            var order = new Order { Id = Guid.NewGuid(), Status = OrderStatus.Processing };
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Price = 10m };

            // Act
            var exception = Record.Exception(() => OrderModificationService.AddProductToOrder(order, product));

            // Assert
            Assert.Null(exception);
        }
    }
}