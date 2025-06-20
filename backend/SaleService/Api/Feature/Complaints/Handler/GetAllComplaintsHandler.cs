using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints;
using SaleService.Api.Feature.Complaints.Query;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;

namespace SaleService.Api.Feature.Complaints.Handler;

public class GetAllComplaintsHandler : IRequestHandler<GetAllComplaintsQuery, BaseResponse<IEnumerable<ComplaintDTO>>>
{
    private readonly IGenericRepository<Complaint> _complaintRepository;

    public GetAllComplaintsHandler(IGenericRepository<Complaint> complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }

    public async Task<BaseResponse<IEnumerable<ComplaintDTO>>> Handle(GetAllComplaintsQuery request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintRepository.GetAllAsync();
        
        var complaintDtos = complaints.Select(c => new ComplaintDTO
        {
            Id = c.Id,
            IdSale = c.IdSale,
            Reason = c.Reason,
            Description = c.Description
        });

        return BaseResponse<IEnumerable<ComplaintDTO>>.SuccessResponse(complaintDtos);
    }
} 