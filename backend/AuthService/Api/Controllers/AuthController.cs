using AuthService.Api.Common;
using AuthService.Api.Feature.Auth.Command;
using AuthService.Api.Feature.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthService.Infrastructure.Services;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;

    public AuthController(IMediator mediator, IJwtService jwtService)
    {
        _mediator = mediator;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<BaseResponse<LoginResponse>> Login([FromBody] LoginCommand command)
    {
        return await _mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<BaseResponse<LoginResponse>> Register([FromBody] RegisterCommand command)
    {
        return await _mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpGet("test-token")]
    public ActionResult<BaseResponse<LoginResponse>> GetTestToken()
    {
        // Crear un token de prueba para testing
        var testUser = new AuthService.Domain.Models.Client
        {
            Id = 1,
            Name = "Usuario",
            LastName = "Prueba",
            Email = "test@example.com"
        };

        var token = _jwtService.GenerateToken(testUser);

        var response = new LoginResponse
        {
            Token = token,
            UserType = "Client",
            UserId = testUser.Id,
            Email = testUser.Email,
            Name = testUser.Name,
            LastName = testUser.LastName
        };

        return Ok(BaseResponse<LoginResponse>.CreateSuccess(response, "Token de prueba generado"));
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<BaseResponse<LoginResponse>> GetCurrentUser()
    {
        var userType = User.FindFirst("UserType")?.Value;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var lastName = User.FindFirst("Surname")?.Value;

        if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(userId))
        {
            return Unauthorized(BaseResponse<LoginResponse>.CreateError("Usuario no autenticado"));
        }

        // Obtener el token del encabezado de autorización
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        return Ok(BaseResponse<LoginResponse>.CreateSuccess(new LoginResponse
        {
            Token = token,
            UserType = userType,
            UserId = int.Parse(userId),
            Email = email ?? string.Empty,
            Name = name ?? string.Empty,
            LastName = lastName ?? string.Empty
        }));
    }
} 