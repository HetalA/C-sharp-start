using System;
using System.Collections.Generic;

namespace flightmvc.Models;

public partial class HetalUsertable
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
