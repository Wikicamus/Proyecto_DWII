using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints.Command;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;

namespace SaleService.Api.Feature.Complaints.Handler;

public class DeleteComplaintHandler : IRequestHandler<DeleteComplaintCommand, BaseResponse<bool>>
{
    private readonly IGenericRepository<Complaint> _complaintRepository;

    public DeleteComplaintHandler(IGenericRepository<Complaint> complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
    {
        // Validar que la queja exista
        var complaint = await _complaintRepository.GetByIdAsync(request.Id);
        if (complaint == null)
        {
            return BaseResponse<bool>.FailureResponse("La queja no existe.");
        }

        await _complaintRepository.RemoveByIdAsync(request.Id);
        return BaseResponse<bool>.SuccessResponse(true, "Queja eliminada exitosamente.");
    }
} 