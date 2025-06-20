using System;
using System.ComponentModel.DataAnnotations;
using AuthService.Api.Common;
using MediatR;

namespace AuthService.Api.Feature.Auth.Command;
public class LogoutCommand : IRequest<BaseResponse<int>>
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}