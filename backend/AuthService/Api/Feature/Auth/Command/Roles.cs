namespace AuthService.Api.Feature.Auth.Command;
public static class Roles
{
    public const string Admin = "Admin";
    public const string Vendedor = "Vendedor";
    
    public static bool IsValidRole(string role)
    {
        return role == Admin || 
               role == Vendedor;
    }
} 