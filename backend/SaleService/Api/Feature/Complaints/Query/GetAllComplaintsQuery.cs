using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints;

namespace SaleService.Api.Feature.Complaints.Query;

public class GetAllComplaintsQuery : IRequest<BaseResponse<IEnumerable<ComplaintDTO>>>
{
} 