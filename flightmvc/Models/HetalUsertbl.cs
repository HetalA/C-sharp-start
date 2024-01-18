using System;
using System.Collections.Generic;

namespace flightmvc.Models;

public partial class HetalUsertbl
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Id { get; set; }
}
