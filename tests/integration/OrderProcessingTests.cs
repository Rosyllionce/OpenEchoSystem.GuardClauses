using System;
using System.Collections.Generic;
using System.Linq;
using OpenEchoSystem.GuardClauses;
using OpenEchoSystem.GuardClauses.Exceptions;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    public class OrderProcessingTests
    {
        private readonly List<Product> _products;

        public OrderProcessingTests()
        {
            _products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product A", Price = 10.0m },
                new Product { Id = Guid.NewGuid(), Name = "Product B", Price = 20.0m }
            };
        }

        private Product? GetProductById(Guid productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        [Fact]
        public void ProcessOrderWithValidOrderProcessesSuccessfully()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 2 }
                },
                Discount = 0.1m,
                Status = OrderStatus.Processing
            };

            // Act
            var exception = Record.Exception(() => ProcessOrder(order));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void ProcessOrderWithNullCustomerThrowsArgumentNullException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = null!,
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 2 }
                }
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithEmptyOrderLinesThrowsArgumentException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>()
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithProductNotFoundThrowsNotFoundException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = Guid.NewGuid(), Quantity = 1 }
                }
            };

            // Act & Assert
            Assert.Throws<NotFoundException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithInvalidEmailThrowsArgumentException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "invalid-email",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 1 }
                }
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithZeroQuantityThrowsArgumentException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 0 }
                }
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithNegativePriceThrowsArgumentException()
        {
            // Arrange
            _products.Add(new Product { Id = Guid.NewGuid(), Name = "Product C", Price = -5.0m });
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products.Last().Id, Quantity = 1 }
                }
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithOutOfRangeDiscountThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 1 }
                },
                Discount = 1.1m
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithInvalidStatusThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 1 }
                },
                Status = (OrderStatus)99
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ProcessOrder(order));
        }

        [Fact]
        public void ProcessOrderWithCustomConditionThrowsArgumentException()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ProductId = _products[0].Id, Quantity = 1001 }
                }
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProcessOrder(order));
        }

        private void ProcessOrder(Order order)
        {
            Guard.Against.Null(order, nameof(order));
            Guard.Against.NullOrWhiteSpace(order.CustomerName, nameof(order.CustomerName));
            Guard.Against.InvalidEmail(order.CustomerEmail, nameof(order.CustomerEmail));
            Guard.Against.NullOrEmpty(order.OrderLines, nameof(order.OrderLines));
            Guard.Against.OutOfRange(order.Discount, 0, 1, nameof(order.Discount));
            Guard.Against.OutOfRange(order.Status, nameof(order.Status));

            decimal total = 0;
            foreach (var line in order.OrderLines)
            {
                Guard.Against.Zero(line.Quantity, nameof(line.Quantity));
                Guard.Against.NotFound(line.ProductId, GetProductById);
                var product = GetProductById(line.ProductId);
                Guard.Against.Negative(product!.Price, nameof(product.Price));

                total += product.Price * line.Quantity;
            }

            Guard.Against.CustomCondition(order.OrderLines.Sum(ol => ol.Quantity) > 1000, "Order cannot exceed 1000 items.");
        }
    }
}