using System;

namespace _net.Models;

public class OrderItemModel
{
    public int Id { get; set; }
    public int ProductQuantity { get; set; }
    public string Price { get; set; }


    public int OrderModelId { get; set; }
    public OrderModel OrderModel {get;set;}


    public int ProductModelId { get; set; }
    public ProductModel ProductModel { get; set; }
}
