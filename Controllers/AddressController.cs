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
    public class AddressController(MyDataContext _context, IMapper _mapper) : ControllerBase
    {
        [HttpPost("createAddress")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDTO addressDTO) {
            try {
                var addressModel = _mapper.Map<CreateAddressDTO, AddressModel>(addressDTO);

                await _context.Addresses.AddAsync(addressModel);

                await _context.SaveChangesAsync();
                return Ok();
            } catch (Exception error) {
                return Problem("Error creating address");
            }
        }

        [HttpGet("getAddress/{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId) {
            return Ok(_context.Addresses.Where(x => x.Id == addressId));
        }

        [HttpGet("GetAllUserAddresses/{userId}")]
        public async Task<IActionResult> GetAllUserAddresses(int userId) {

            var addressDTO = _mapper.Map<List<AddressModel>, List<AddressDTO>>(_context.Addresses.Where(x => x.UserModelId == userId).ToList());

            return Ok(addressDTO);
        }
    }
}
