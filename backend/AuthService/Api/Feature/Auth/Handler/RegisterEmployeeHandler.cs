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

public class RegisterEmployeeHandler : IRequestHandler<RegisterCommand, BaseResponse<LoginResponse>>
{
    private readonly db_Context _context;
    private readonly IJwtService _jwtService;

    public RegisterEmployeeHandler(db_Context context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<LoginResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.UserType != "Employee")
        {
            return BaseResponse<LoginResponse>.CreateError("Tipo de usuario inválido");
        }

        if (!Roles.IsValidRole(request.UserType))
        {
            return BaseResponse<LoginResponse>.CreateError("Rol de empleado inválido");
        }

        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

        if (existingEmployee != null)
        {
            return BaseResponse<LoginResponse>.CreateError("El correo electrónico ya está registrado");
        }

        var employee = new Employee
        {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GenerateToken(employee);

        var response = new LoginResponse
        {
            Token = token,
            UserType = "Employee",
            UserId = employee.Id,
            Email = employee.Email,
            Name = employee.Name,
            LastName = employee.LastName
        };

        return BaseResponse<LoginResponse>.CreateSuccess(response, "Empleado registrado exitosamente");
    }
} 