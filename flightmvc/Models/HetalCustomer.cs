using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightmvc.Models;

public partial class HetalCustomer
{
    public int CustomerId { get; set; }
    [Required(ErrorMessage ="Name is required")]
    public string CustomerName { get; set; } 
    [Required(ErrorMessage ="Location is required")]
    public string Location { get; set; } 

    public virtual ICollection<HetalBooking> HetalBookings { get; set; } = new List<HetalBooking>();
}
