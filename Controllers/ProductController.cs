using _net.DataContext;
using _net.DTOs;
using _net.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController(MyDataContext _context, IMapper _mapper) : ControllerBase
    {
        [HttpGet("listProducts/{page}")]
        public IActionResult ListProducts(int page) {
            var prods = _context.Products.Take(10).Skip(page).OrderBy(x => x.Id).ToList();

            var prodsDTOs = _mapper.Map<List<ProductModel>, List<ProductDTO>>(prods);

            return Ok(prodsDTOs);
        }

    }
}
 