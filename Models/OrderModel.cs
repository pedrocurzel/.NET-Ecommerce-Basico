using System;

namespace _net.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int UserModelId { get; set; }
    public int AddressModelId { get; set; }
    public string FullOrderPrice { get; set; }
    public string Status {get; set;}

    public ICollection<OrderItemModel> orderItemModels { get; } = new List<OrderItemModel>();
}
