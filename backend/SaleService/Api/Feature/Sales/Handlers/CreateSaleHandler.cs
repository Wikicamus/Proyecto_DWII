using System.Net.Http;
using System.Text.Json;
using SaleService.Domain.Models;
using SaleService.Api.Feature.Sales.Commands;
using SaleService.Api.Common;
using MediatR;
using SaleService.Domain.Interfaces;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, BaseResponse<int>>
{
    private readonly IGenericRepository<Sale> _repository;
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateSaleHandler(IGenericRepository<Sale> repository, IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<BaseResponse<int>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        // 1. Obtener el precio del producto desde el microservicio de productos
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:5291/api/Product/{request.IdProduct}");
        if (!response.IsSuccessStatusCode)
            return BaseResponse<int>.FailureResponse("No se pudo obtener el precio del producto");

        var json = await response.Content.ReadAsStringAsync();
        var productResponse = JsonSerializer.Deserialize<ProductResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (productResponse == null || productResponse.Data == null)
            return BaseResponse<int>.FailureResponse("Producto no encontrado");

        // 2. Calcular el total
        double total = productResponse.Data.Price * request.Units;

        // 3. Crear la venta
        var sale = new Sale
        {
            Date = request.Date,
            IdClient = request.IdClient,
            IdProduct = request.IdProduct,
            Units = request.Units,
            Total = total
        };

        await _repository.AddAsync(sale);

        return BaseResponse<int>.SuccessResponse(sale.Id);
    }

    // DTO temporal para deserializar la respuesta del microservicio de productos
    private class ProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ProductDTO Data { get; set; }
    }

    private class ProductDTO
    {
        public double Price { get; set; }
    }
}