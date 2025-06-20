using System.ComponentModel.DataAnnotations;
using AuthService.Api.Common;
using AuthService.Api.Feature.Auth.Models;
using MediatR;

namespace AuthService.Api.Feature.Auth.Command;

public class RegisterCommand : IRequest<BaseResponse<LoginResponse>>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string UserType { get; set; } = string.Empty;

    public string? Address { get; set; }
    
    public string? Phone { get; set; }
} 