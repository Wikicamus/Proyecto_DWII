using System;

namespace Api.Features.Proveedores.Abstraction
{
    public class SupplierDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
} 