using InventoryService.Api.Common;
using InventoryService.Domain.Models;
using InventoryService.Api.Features.Proveedores.Queries;
using Api.Features.Proveedores.Abstraction;
using MediatR;
using InventoryService.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryService.Api.Features.Proveedores.Handlers
{
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersQuery, BaseResponse<IEnumerable<SupplierDto>>>
    {
        private readonly IGenericRepository<Supplier> _repository;

        public GetAllSuppliersHandler(IGenericRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<IEnumerable<SupplierDto>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _repository.GetAllAsync();
            var supplierDtos = suppliers.Select(s => new SupplierDto
            {
                Name = s.Name,
                Phone = s.Phone.ToString(),
                Address = s.Address
            });

            return BaseResponse<IEnumerable<SupplierDto>>.SuccessResponse(supplierDtos);
        }
    }
} 