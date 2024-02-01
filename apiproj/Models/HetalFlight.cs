using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiproj.Models;

public partial class HetalFlight
{
    public int FlightId { get; set; }
    [Required(ErrorMessage = "Please enter a valid name")]
    public string FlightName { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a valid airline name")]
    public string Airline { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a valid source")]
    public string Source { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a valid destination")]
    public string Destination { get; set; } = null!;
    [Required(ErrorMessage = "Please enter the cost per ticket")]
    public double Rate { get; set; }
    [RegularExpression(@"^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage="Please enter time in 24-hr format")]
    public string Departure { get; set; } = null!;
    [RegularExpression(@"^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage="Please enter time in 24-hr format")]
    public string Arrival { get; set; } = null!;
}
