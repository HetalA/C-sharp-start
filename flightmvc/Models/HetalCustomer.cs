using System;
using System.Collections.Generic;

namespace flightmvc.Models;

public partial class HetalCustomer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<HetalBooking> HetalBookings { get; set; } = new List<HetalBooking>();
}
