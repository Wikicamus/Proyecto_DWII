using InventoryService.Api.Common;
using InventoryService.Api.Features.Productos.Commands;
using InventoryService.Api.Features.Productos.Queries;
using Api.Features.Productos.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace InventoryService.Api.Features.Productos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto product)
        {
            try
            {
                // Validar token de autorización
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                // Extraer información del usuario del token
                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "Email desconocido";

                _logger.LogInformation("Usuario {Username} ({Email}) creando producto: {Name}", username, email, product.Name);
                
                // Logging de datos recibidos
                _logger.LogInformation("Datos del producto: Name={Name}, Description={Description}, Price={Price}, Category={Category}, Stock={Stock}, SupplierId={SupplierId}", 
                    product.Name, product.Description, product.Price, product.Category, product.Stock, product.SupplierId);
                
                var command = new CreateProductCommand
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    Stock = product.Stock,
                    SupplierId = product.SupplierId
                };

                var result = await _mediator.Send(command);
                
                if (result.Success)
                {
                    _logger.LogInformation("Producto creado exitosamente con ID: {ProductId} por usuario: {Username}", result.Data, username);
                }
                else
                {
                    _logger.LogWarning("Error al crear producto: {Error} por usuario: {Username}", result.Message, username);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al crear producto");
                return StatusCode(500, BaseResponse<int>.FailureResponse("Error interno del servidor"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // Validar token de autorización
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} consultando producto con ID: {Id}", username, id);

                var query = new GetProductByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                
                if (result.Success)
                {
                    _logger.LogInformation("Producto con ID {Id} consultado exitosamente por usuario: {Username}", id, username);
                }
                else
                {
                    _logger.LogWarning("Producto con ID {Id} no encontrado por usuario: {Username}", id, username);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al consultar producto con ID: {Id}", id);
                return StatusCode(500, BaseResponse<BaseCommandDTO>.FailureResponse("Error interno del servidor"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Validar token de autorización
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} consultando todos los productos", username);

                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query);
                
                if (result.Success)
                {
                    var productCount = result.Data?.Count() ?? 0;
                    _logger.LogInformation("Se consultaron {Count} productos exitosamente por usuario: {Username}", productCount, username);
                }
                else
                {
                    _logger.LogWarning("Error al consultar productos: {Error} por usuario: {Username}", result.Message, username);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al consultar todos los productos");
                return StatusCode(500, BaseResponse<IEnumerable<BaseCommandDTO>>.FailureResponse("Error interno del servidor"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto product)
        {
            try
            {
                // Validar token de autorización
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} actualizando producto con ID: {Id}", username, id);

                var command = new UpdateProductCommand
                {
                    Id = id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    Stock = product.Stock,
                    SupplierId = product.SupplierId
                };

                var result = await _mediator.Send(command);
                
                if (result.Success)
                {
                    _logger.LogInformation("Producto con ID {Id} actualizado exitosamente por usuario: {Username}", id, username);
                }
                else
                {
                    _logger.LogWarning("Error al actualizar producto con ID {Id}: {Error} por usuario: {Username}", id, result.Message, username);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al actualizar producto con ID: {Id}", id);
                return StatusCode(500, BaseResponse<bool>.FailureResponse("Error interno del servidor"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Validar token de autorización
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} eliminando producto con ID: {Id}", username, id);

                var command = new DeleteProductCommand { Id = id };
                var result = await _mediator.Send(command);
                
                if (result.Success)
                {
                    _logger.LogInformation("Producto con ID {Id} eliminado exitosamente por usuario: {Username}", id, username);
                }
                else
                {
                    _logger.LogWarning("Error al eliminar producto con ID {Id}: {Error} por usuario: {Username}", id, result.Message, username);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al eliminar producto con ID: {Id}", id);
                return StatusCode(500, BaseResponse<bool>.FailureResponse("Error interno del servidor"));
            }
        }
    }
} 