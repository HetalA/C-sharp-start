using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightmvc.Models;

public partial class HetalFlight
{
    public int FlightId { get; set; }
    [Required(ErrorMessage ="Required field")]
    public string? FlightName { get; set; } 
    [Required(ErrorMessage ="Required field")]
    public string? Airline { get; set; }
    [Required(ErrorMessage ="Required field")]
    public string? Source { get; set; } 
    [Required(ErrorMessage ="Required field")]
    public string? Destination { get; set; } 

    public double Rate { get; set; }

    public virtual ICollection<HetalBooking> HetalBookings { get; set; } = new List<HetalBooking>();
}
