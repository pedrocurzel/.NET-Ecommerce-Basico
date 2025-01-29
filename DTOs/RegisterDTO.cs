using System;
using System.ComponentModel.DataAnnotations;

namespace _net.DTOs;

public class RegisterDTO
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}
