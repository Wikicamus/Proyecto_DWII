using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints;
using SaleService.Api.Feature.Complaints.Query;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;

namespace SaleService.Api.Feature.Complaints.Handler;

public class GetComplaintByIdHandler : IRequestHandler<GetComplaintByIdQuery, BaseResponse<ComplaintDTO>>
{
    private readonly IGenericRepository<Complaint> _complaintRepository;

    public GetComplaintByIdHandler(IGenericRepository<Complaint> complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }

    public async Task<BaseResponse<ComplaintDTO>> Handle(GetComplaintByIdQuery request, CancellationToken cancellationToken)
    {
        var complaint = await _complaintRepository.GetByIdAsync(request.Id);
        
        if (complaint == null)
        {
            return BaseResponse<ComplaintDTO>.FailureResponse("La queja no existe.");
        }

        var complaintDto = new ComplaintDTO
        {
            Id = complaint.Id,
            IdSale = complaint.IdSale,
            Reason = complaint.Reason,
            Description = complaint.Description
        };

        return BaseResponse<ComplaintDTO>.SuccessResponse(complaintDto);
    }
} 