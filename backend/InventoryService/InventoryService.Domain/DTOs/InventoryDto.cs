using System;

namespace InventoryService.Domain.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public DateTime MovementDate { get; set; }
    }
} 