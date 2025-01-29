using System;

namespace _net.DTOs;

public class CreateOrderDTO
{
    public int UserModelId { get; set; }
    public int AddressModelId { get; set; }

    public List<ProductDTO> Products { get; set; }
}
