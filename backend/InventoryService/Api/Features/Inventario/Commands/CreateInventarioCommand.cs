using System;
using MediatR;
using InventoryService.Api.Common;
using InventoryService.Api.Features.Inventario.Abstraction;

namespace InventoryService.Api.Features.Inventario.Commands
{
    public class CreateInventarioCommand : InventarioDto, IRequest<BaseResponse<int>>
    {
    }
} 