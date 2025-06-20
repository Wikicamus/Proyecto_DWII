using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints.Command;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;

namespace SaleService.Api.Feature.Complaints.Handler;

public class UpdateComplaintHandler : IRequestHandler<UpdateComplaintCommand, BaseResponse<bool>>
{
    private readonly IGenericRepository<Complaint> _complaintRepository;
    private readonly IGenericRepository<Sale> _saleRepository;

    public UpdateComplaintHandler(
        IGenericRepository<Complaint> complaintRepository,
        IGenericRepository<Sale> saleRepository)
    {
        _complaintRepository = complaintRepository;
        _saleRepository = saleRepository;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
    {
        // Validar que la queja exista
        var existingComplaint = await _complaintRepository.GetByIdAsync(request.Id);
        if (existingComplaint == null)
        {
            return BaseResponse<bool>.FailureResponse("La queja no existe.");
        }

        // Validar que la venta exista
        var sale = await _saleRepository.GetByIdAsync(request.IdSale);
        if (sale == null)
        {
            return BaseResponse<bool>.FailureResponse("La venta especificada no existe.");
        }

        // Actualizar la queja
        existingComplaint.IdSale = request.IdSale;
        existingComplaint.Reason = request.Reason;
        existingComplaint.Description = request.Description;

        _complaintRepository.Update(existingComplaint);
        return BaseResponse<bool>.SuccessResponse(true, "Queja actualizada exitosamente.");
    }
} 