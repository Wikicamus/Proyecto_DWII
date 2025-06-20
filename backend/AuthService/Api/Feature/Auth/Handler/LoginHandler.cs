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

public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<LoginResponse>>
{
    private readonly db_Context _context;
    private readonly IJwtService _jwtService;

    public LoginHandler(db_Context context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Intentar encontrar un cliente primero
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);

        if (client != null && BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
        {
            var token = _jwtService.GenerateToken(client);
            return BaseResponse<LoginResponse>.CreateSuccess(new LoginResponse
            {
                Token = token,
                UserType = "Client",
                UserId = client.Id,
                Email = client.Email,
                Name = client.Name,
                LastName = client.LastName
            });
        }

        // Si no es cliente, buscar empleado
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

        if (employee != null && BCrypt.Net.BCrypt.Verify(request.Password, employee.Password))
        {
            var token = _jwtService.GenerateToken(employee);
            return BaseResponse<LoginResponse>.CreateSuccess(new LoginResponse
            {
                Token = token,
                UserType = "Employee",
                UserId = employee.Id,
                Email = employee.Email,
                Name = employee.Name,
                LastName = employee.LastName
            });
        }

        return BaseResponse<LoginResponse>.CreateError("Credenciales inválidas");
    }
} 