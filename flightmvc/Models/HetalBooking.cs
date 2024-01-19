using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightmvc.Models;

public partial class HetalBooking
{
    public int BookingId { get; set; }

    public int FlightId { get; set; }

    public int CustomerId { get; set; }
    [Required(ErrorMessage ="Please choose a valid date")]
    public DateOnly? BookingDate { get; set; }

    public int NoOfPassengers { get; set; }

    public double TotalCost { get; set; }
    [NotMapped]
    public string Source { get; set; }
    [NotMapped]
    public string Destination { get; set; } 

    public virtual HetalCustomer Customer { get; set; } = null!;

    public virtual HetalFlight Flight { get; set; } = null!;
}
