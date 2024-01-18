using System;
using System.Collections.Generic;

namespace flightmvc.Models;

public partial class HetalFlight
{
    public int FlightId { get; set; }

    public string FlightName { get; set; } = null!;

    public string Airline { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public double Rate { get; set; }

    public virtual ICollection<HetalBooking> HetalBookings { get; set; } = new List<HetalBooking>();
}
