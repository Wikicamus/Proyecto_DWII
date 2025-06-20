using System.ComponentModel.DataAnnotations;
using AuthService.Api.Common;
using AuthService.Api.Feature.Auth.Models;
using MediatR;

namespace AuthService.Api.Feature.Auth.Command;

public class LoginCommand : IRequest<BaseResponse<LoginResponse>>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}