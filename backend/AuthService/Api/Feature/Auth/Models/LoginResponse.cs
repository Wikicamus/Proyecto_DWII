using System;

namespace AuthService.Api.Feature.Auth.Models;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
} 