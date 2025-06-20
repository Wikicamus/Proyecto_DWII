using MediatR;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.Complaints.Command;

public class DeleteComplaintCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }
} 