using System;

namespace Api.Features.Productos.Abstraction
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SupplierId { get; set; }
        public string Category { get; set; } = string.Empty;
    }
} 