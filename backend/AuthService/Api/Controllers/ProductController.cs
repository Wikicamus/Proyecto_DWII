using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Api.Common;
using AuthService.Api.Interface;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Linq;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMicroservicesService _microservicesService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IMicroservicesService microservicesService)
        {
            _logger = logger;
            _microservicesService = microservicesService;
        }

        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} obteniendo todos los productos", username);

                var products = await _microservicesService.GetAllProducts();
                
                return Ok(BaseResponse<List<ProductResponse>>.CreateSuccess(products, "Productos obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos");
                return StatusCode(500, BaseResponse<List<ProductResponse>>.CreateError("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene un producto por su ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Producto encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} obteniendo producto con ID: {Id}", username, id);

                var product = await _microservicesService.GetProductById(id);
                
                if (product == null)
                {
                    return NotFound(BaseResponse<ProductResponse>.CreateError($"Producto con ID {id} no encontrado"));
                }
                
                return Ok(BaseResponse<ProductResponse>.CreateSuccess(product, "Producto obtenido exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto con ID: {Id}", id);
                return StatusCode(500, BaseResponse<ProductResponse>.CreateError("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        /// <param name="productRequest">Datos del producto a crear</param>
        /// <returns>Producto creado</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(BaseResponse<ProductResponse>.CreateError("Datos de entrada inválidos"));
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} creando nuevo producto: {Name}", username, productRequest.Name);

                var createdProduct = await _microservicesService.CreateProduct(productRequest);
                
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, 
                    BaseResponse<ProductResponse>.CreateSuccess(createdProduct, "Producto creado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto: {Name}", productRequest.Name);
                return StatusCode(500, BaseResponse<ProductResponse>.CreateError("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        /// <param name="id">ID del producto a actualizar</param>
        /// <param name="productRequest">Nuevos datos del producto</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(BaseResponse<bool>.CreateError("Datos de entrada inválidos"));
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} actualizando producto con ID: {Id}", username, id);

                var success = await _microservicesService.UpdateProduct(id, productRequest);
                
                if (!success)
                {
                    return NotFound(BaseResponse<bool>.CreateError($"Producto con ID {id} no encontrado"));
                }
                
                return Ok(BaseResponse<bool>.CreateSuccess(true, "Producto actualizado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto con ID: {Id}", id);
                return StatusCode(500, BaseResponse<bool>.CreateError("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="id">ID del producto a eliminar</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} eliminando producto con ID: {Id}", username, id);

                var success = await _microservicesService.DeleteProduct(id);
                
                if (!success)
                {
                    return NotFound(BaseResponse<bool>.CreateError($"Producto con ID {id} no encontrado"));
                }
                
                return Ok(BaseResponse<bool>.CreateSuccess(true, "Producto eliminado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto con ID: {Id}", id);
                return StatusCode(500, BaseResponse<bool>.CreateError("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene productos por categoría
        /// </summary>
        /// <param name="category">Categoría de los productos</param>
        /// <returns>Lista de productos de la categoría</returns>
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} obteniendo productos por categoría: {Category}", username, category);

                var products = await _microservicesService.GetProductsByCategory(category);
                
                return Ok(BaseResponse<List<ProductResponse>>.CreateSuccess(products, $"Productos de la categoría {category} obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos por categoría: {Category}", category);
                return StatusCode(500, BaseResponse<List<ProductResponse>>.CreateError("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene productos por proveedor
        /// </summary>
        /// <param name="supplierId">ID del proveedor</param>
        /// <returns>Lista de productos del proveedor</returns>
        [HttpGet("supplier/{supplierId}")]
        public async Task<IActionResult> GetProductsBySupplier(int supplierId)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Token de autorización requerido");
                }

                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido";
                _logger.LogInformation("Usuario {Username} obteniendo productos por proveedor: {SupplierId}", username, supplierId);

                var products = await _microservicesService.GetProductsBySupplier(supplierId);
                
                return Ok(BaseResponse<List<ProductResponse>>.CreateSuccess(products, $"Productos del proveedor {supplierId} obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos por proveedor: {SupplierId}", supplierId);
                return StatusCode(500, BaseResponse<List<ProductResponse>>.CreateError("Error interno del servidor"));
            }
        }
    }
}