using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightmvc.Models;

public partial class HetalUsertable
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter a valid email")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a username")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; } = null!;
    [NotMapped]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public string? Location { get; set; }
}
