using System;

namespace _net.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public int Quantity { get; set; } = 1;
}
