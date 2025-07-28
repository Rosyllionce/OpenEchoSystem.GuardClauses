using System;
using System.Collections.Generic;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public List<OrderLine> OrderLines { get; set; } = new();
        public decimal Discount { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class OrderLine
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid SupplierId { get; set; }

        public Product() { }

        public Product(Guid id, string name, decimal price, Guid supplierId)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.Negative(price, nameof(price));
            Guard.Against.Zero(price, nameof(price));
            Guard.Against.Empty(supplierId, nameof(supplierId));

            Id = id;
            Name = name;
            Price = price;
            SupplierId = supplierId;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Cancelled
    }
}