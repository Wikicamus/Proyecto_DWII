using System;
using System.Threading;
using System.Threading.Tasks;
using AuthService.Api.Common;
using AuthService.Api.Feature.Auth.Command;
using AuthService.Api.Feature.Auth.Models;
using AuthService.Domain.Models;
using AuthService.Infrastructure.Data;
using AuthService.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Api.Feature.Auth.Handler;

public class RegisterClientHandler : IRequestHandler<RegisterCommand, BaseResponse<LoginResponse>>
{
    private readonly db_Context _context;
    private readonly IJwtService _jwtService;

    public RegisterClientHandler(db_Context context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<LoginResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.UserType != "Client")
        {
            return BaseResponse<LoginResponse>.CreateError("Tipo de usuario inválido");
        }

        var existingClient = await _context.Clients
            .FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);

        if (existingClient != null)
        {
            return BaseResponse<LoginResponse>.CreateError("El correo electrónico ya está registrado");
        }

        var client = new Client
        {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Address = request.Address ?? string.Empty,
            Phone = request.Phone != null ? int.Parse(request.Phone) : 0
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GenerateToken(client);

        var response = new LoginResponse
        {
            Token = token,
            UserType = "Client",
            UserId = client.Id,
            Email = client.Email,
            Name = client.Name,
            LastName = client.LastName
        };

        return BaseResponse<LoginResponse>.CreateSuccess(response, "Cliente registrado exitosamente");
    }
} 