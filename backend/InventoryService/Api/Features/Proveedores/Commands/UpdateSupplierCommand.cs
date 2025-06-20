using System.Text.Json.Serialization;
using Api.Features.Proveedores.Abstraction;
using InventoryService.Api.Common;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Commands
{
    public class UpdateSupplierCommand : SupplierDto, IRequest<BaseResponse<int>>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
} 