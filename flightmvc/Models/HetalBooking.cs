using System;
using System.Collections.Generic;

namespace flightmvc.Models;

public partial class HetalBooking
{
    public int BookingId { get; set; }

    public int FlightId { get; set; }

    public int CustomerId { get; set; }

    public DateTime BookingDate { get; set; }

    public int NoOfPassengers { get; set; }

    public double TotalCost { get; set; }

    public virtual HetalCustomer Customer { get; set; } = null!;

    public virtual HetalFlight Flight { get; set; } = null!;
}
