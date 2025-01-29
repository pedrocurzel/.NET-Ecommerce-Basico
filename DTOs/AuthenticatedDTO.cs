using System;

namespace _net.DTOs;

public class AuthenticatedDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}
