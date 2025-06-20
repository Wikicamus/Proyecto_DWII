using System.Text;
using System.Text.Json;
using AuthService.Models;
using Microsoft.AspNetCore.Http;

namespace AuthService.Api.Interface
{
    public interface IMicroservicesService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse?> GetProductById(int id);
        Task<ProductResponse> CreateProduct(ProductRequest product);
        Task<bool> UpdateProduct(int id, ProductRequest product);
        Task<bool> DeleteProduct(int id);
        Task<List<ProductResponse>> GetProductsByCategory(string category);
        Task<List<ProductResponse>> GetProductsBySupplier(int supplierId);
    }

    public class MicroservicesService : IMicroservicesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MicroservicesService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MicroservicesService(HttpClient httpClient, IConfiguration configuration, ILogger<MicroservicesService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetAuthHeader()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        }

        private async Task<TResponse?> GetFromServiceAsync<TResponse>(string url) where TResponse : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var authHeader = GetAuthHeader();

                if (!string.IsNullOrWhiteSpace(authHeader))
                    request.Headers.Add("Authorization", authHeader);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Respuesta del servicio: {ResponseContent}", responseContent);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        // Intentar deserializar como BaseResponse primero
                        var baseResponse = JsonSerializer.Deserialize<BaseResponseWrapper<TResponse>>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (baseResponse?.Data != null)
                        {
                            return baseResponse.Data;
                        }

                        // Si no es BaseResponse, intentar deserializar directamente
                        return JsonSerializer.Deserialize<TResponse>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error al deserializar respuesta JSON: {ResponseContent}", responseContent);
                        return default;
                    }
                }

                _logger.LogWarning("Fallo al GET en {Url}. Status: {StatusCode}. Response: {Response}", 
                    url, response.StatusCode, responseContent);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al hacer GET en {Url}", url);
                return default;
            }
        }

        private async Task<TResponse?> PostToServiceAsync<TRequest, TResponse>(string url, TRequest data) where TResponse : class
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                _logger.LogInformation("Enviando POST a {Url} con datos: {Data}", url, json);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                var authHeader = GetAuthHeader();
                if (!string.IsNullOrWhiteSpace(authHeader))
                    request.Headers.Add("Authorization", authHeader);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Respuesta exitosa de POST: {Response}", responseContent);
                    
                    try
                    {
                        // Intentar deserializar como BaseResponse primero
                        var baseResponse = JsonSerializer.Deserialize<BaseResponseWrapper<TResponse>>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (baseResponse?.Data != null)
                        {
                            return baseResponse.Data;
                        }

                        // Si no es BaseResponse, intentar deserializar directamente
                        return JsonSerializer.Deserialize<TResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error al deserializar respuesta JSON: {ResponseContent}", responseContent);
                        return default;
                    }
                }

                _logger.LogWarning("Fallo al POST en {Url}. Status: {StatusCode}. Response: {Response}", 
                    url, response.StatusCode, responseContent);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al hacer POST en {Url}", url);
                return default;
            }
        }

        private async Task<bool> PutToServiceAsync<TRequest>(string url, TRequest data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = content
                };

                var authHeader = GetAuthHeader();
                if (!string.IsNullOrWhiteSpace(authHeader))
                    request.Headers.Add("Authorization", authHeader);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        // Intentar deserializar como BaseResponse primero
                        var baseResponse = JsonSerializer.Deserialize<BaseResponseWrapper<bool>>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (baseResponse?.Data != null)
                        {
                            return baseResponse.Data;
                        }

                        // Si no es BaseResponse, intentar deserializar directamente
                        return JsonSerializer.Deserialize<bool>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error al deserializar respuesta JSON: {ResponseContent}", responseContent);
                        return false;
                    }
                }

                _logger.LogWarning("Fallo al PUT en {Url}. Status: {StatusCode}. Response: {Response}", 
                    url, response.StatusCode, responseContent);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al hacer PUT en {Url}", url);
                return false;
            }
        }

        private async Task<bool> DeleteFromServiceAsync(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, url);

                var authHeader = GetAuthHeader();
                if (!string.IsNullOrWhiteSpace(authHeader))
                    request.Headers.Add("Authorization", authHeader);

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al hacer DELETE en {Url}", url);
                return false;
            }
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product";
            
            var result = await GetFromServiceAsync<List<ProductResponse>>(url);
            return result ?? new List<ProductResponse>();
        }

        public async Task<ProductResponse?> GetProductById(int id)
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product/{id}";
            
            return await GetFromServiceAsync<ProductResponse>(url);
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest product)
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product";
            
            // Crear un objeto anónimo con el mapeo correcto
            var productData = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Stock = product.Stock,
                SupplierId = product.SupplierId  // Mapear SupplierId a SupplierId
            };
            
            _logger.LogInformation("Datos del producto a enviar: Name={Name}, Description={Description}, Price={Price}, Category={Category}, Stock={Stock}, SupplierId={SupplierId}", 
                productData.Name, productData.Description, productData.Price, productData.Category, productData.Stock, productData.SupplierId);
            
            var result = await PostToServiceAsync<object, ProductResponse>(url, productData);
            return result ?? new ProductResponse();
        }

        public async Task<bool> UpdateProduct(int id, ProductRequest product)
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product/{id}";
            
            var productData = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Stock = product.Stock,
                SupplierId = product.SupplierId
            };
            
            return await PutToServiceAsync(url, productData);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product/{id}";
            
            return await DeleteFromServiceAsync(url);
        }

        public async Task<List<ProductResponse>> GetProductsByCategory(string category)
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product/category/{category}";
            
            var result = await GetFromServiceAsync<List<ProductResponse>>(url);
            return result ?? new List<ProductResponse>();
        }

        public async Task<List<ProductResponse>> GetProductsBySupplier(int supplierId)
        {
            var baseUrl = _configuration["MicroservicesUrls:ProductService"];
            var url = $"{baseUrl}/api/Product/supplier/{supplierId}";
            
            var result = await GetFromServiceAsync<List<ProductResponse>>(url);
            return result ?? new List<ProductResponse>();
        }
    }

    // Clase auxiliar para deserializar BaseResponse
    public class BaseResponseWrapper<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}