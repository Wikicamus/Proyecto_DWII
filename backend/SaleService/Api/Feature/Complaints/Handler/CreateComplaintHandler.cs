using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints.Command;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;

namespace SaleService.Api.Feature.Complaints.Handler;

public class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, BaseResponse<int>>
{
    private readonly IGenericRepository<Complaint> _complaintRepository;
    private readonly IGenericRepository<Sale> _saleRepository;

    public CreateComplaintHandler(
        IGenericRepository<Complaint> complaintRepository,
        IGenericRepository<Sale> saleRepository)
    {
        _complaintRepository = complaintRepository;
        _saleRepository = saleRepository;
    }

    public async Task<BaseResponse<int>> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        // Validar que la venta exista
        var sale = await _saleRepository.GetByIdAsync(request.IdSale);
        if (sale == null)
        {
            return BaseResponse<int>.FailureResponse("La venta especificada no existe.");
        }

        var complaint = new Complaint
        {
            IdSale = request.IdSale,
            Reason = request.Reason,
            Description = request.Description
        };

        await _complaintRepository.AddAsync(complaint);
        return BaseResponse<int>.SuccessResponse(complaint.Id);
    }
} 