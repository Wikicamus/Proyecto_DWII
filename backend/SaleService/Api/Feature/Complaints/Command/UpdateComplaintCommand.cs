using MediatR;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.Complaints.Command;

public class UpdateComplaintCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }
    public int IdSale { get; set; }
    public string Reason { get; set; } = null!;
    public string Description { get; set; } = null!;
} 