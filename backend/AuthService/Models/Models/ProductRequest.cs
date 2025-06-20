using System.ComponentModel.DataAnnotations;

namespace AuthService.Models;

public class ProductRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double Price { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    public int Stock { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del proveedor debe ser válido")]
    public int SupplierId { get; set; }
} 