using _net.DataContext;
using _net.DTOs;
using _net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(MyDataContext _context) : ControllerBase
    {
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO) {
            try {
                await _context.Database.BeginTransactionAsync();

                OrderModel order = new OrderModel {
                    AddressModelId = createOrderDTO.AddressModelId,
                    Status = "Paid",
                    UserModelId = createOrderDTO.UserModelId,
                    FullOrderPrice = createOrderDTO.Products.Aggregate(0, (acc, x) => acc += Int32.Parse(x.Price) * x.Quantity).ToString(),
                };
                
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                List<OrderItemModel> orderItems = new List<OrderItemModel>();
                foreach(ProductDTO prod in createOrderDTO.Products) {
                    orderItems.Add(new OrderItemModel {
                        OrderModelId = order.Id,
                        ProductModelId = prod.Id,
                        ProductQuantity = prod.Quantity,
                        Price = (prod.Quantity * Int32.Parse(prod.Price)).ToString(),
                    });
                }

                await _context.OrderItems.AddRangeAsync(orderItems);
                await _context.SaveChangesAsync();
                
                await _context.Database.CommitTransactionAsync();

                return Ok(order);
            } catch (Exception e) {
                await _context.Database.RollbackTransactionAsync();

                return BadRequest(e.Message);
            }
        }
    }
}
