using System;

namespace _net.DTOs;

public class CreateAddressDTO
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Code { get; set; }

    public int UserModelId { get; set; }
}
