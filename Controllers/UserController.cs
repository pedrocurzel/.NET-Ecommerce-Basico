using _net.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(MyDataContext _context) : ControllerBase
    {
        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id) {
            var u = await _context.Users.Include(x => x.addresses).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(u);
        }
    }
}
