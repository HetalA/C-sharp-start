using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightmvc.Models;

public partial class HetalUsertable
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Please enter an email")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Please enter a valid email address")]
    public string? Email { get; set; }
    [Required(ErrorMessage ="Username is required")]
    public string? Username { get; set; } 
    [Required(ErrorMessage ="Password is required")]
    public string? Password { get; set; } 

    [NotMapped]
    [Compare("Password",ErrorMessage ="Passwords do not match")]
    public string? ConfirmPassword { get; set; } 
}
