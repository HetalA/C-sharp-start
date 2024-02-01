using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiproj.Models;

public partial class HetalBooking
{
    public int BookingId { get; set; }

    public int FlightId { get; set; }

    public int CustomerId { get; set; }
    [Required(ErrorMessage = "Please enter a date")]
    public DateTime? BookingDate { get; set; }
    [Required(ErrorMessage = "Please enter the number of passengers")]
    public int NoOfPassengers { get; set; }

    public double TotalCost { get; set; }
    [Required(ErrorMessage = "Please enter a valid source")]
    [NotMapped]
    public string Source { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a valid destination")]
    [NotMapped]
    public string Destination { get; set; } = null!;
    [NotMapped]
    public string Discount { get; set;} = null!;
    public virtual HetalFlight Flight { get; set; } = null!;
}
