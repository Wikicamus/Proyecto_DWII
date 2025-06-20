using System;
using System.Collections.Generic;

namespace AuthService.Domain.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

}
