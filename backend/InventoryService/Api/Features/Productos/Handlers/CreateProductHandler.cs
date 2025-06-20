using InventoryService.Api.Common;
using InventoryService.Api.Features.Productos.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using MediatR;

namespace InventoryService.Api.Features.Productos.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, BaseResponse<int>>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IGenericRepository<Supplier> _supplierRepository;

        public CreateProductHandler(IGenericRepository<Product> repository, IGenericRepository<Supplier> supplierRepository)
        {
            _repository = repository;
            _supplierRepository = supplierRepository;
        }   

        public async Task<BaseResponse<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"CreateProductHandler - Datos recibidos: Name={request.Name}, Description={request.Description}, Price={request.Price}, Category={request.Category}, Stock={request.Stock}, SupplierId={request.SupplierId}");
                
                // Verificar si el proveedor existe
                var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId);
                if (supplier == null)
                {
                    Console.WriteLine($"Error: Proveedor con ID {request.SupplierId} no encontrado");
                    return BaseResponse<int>.FailureResponse($"Proveedor con ID {request.SupplierId} no encontrado");
                }
                
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = (double)request.Price,
                    Category = request.Category,
                    Stock = request.Stock,
                    IdSupplier = request.SupplierId
                };

                Console.WriteLine($"Producto a crear: Name={product.Name}, Description={product.Description}, Price={product.Price}, Category={product.Category}, Stock={product.Stock}, IdSupplier={product.IdSupplier}");

                await _repository.AddAsync(product);
                Console.WriteLine($"Producto creado con ID: {product.Id}");
                return BaseResponse<int>.SuccessResponse(product.Id);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error en CreateProductHandler: {ex.Message}");
                return BaseResponse<int>.FailureResponse(ex.Message);
            }
        }
    }
} 