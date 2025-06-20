using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints;

namespace SaleService.Api.Feature.Complaints.Query;

public class GetComplaintByIdQuery : IRequest<BaseResponse<ComplaintDTO>>
{
    public int Id { get; set; }
} 