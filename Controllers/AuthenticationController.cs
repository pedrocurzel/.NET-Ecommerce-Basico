using System.Security.Cryptography;
using System.Text;
using _net.DataContext;
using _net.DTOs;
using _net.Interfaces;
using _net.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(MyDataContext _context, IHandleToken _handleToken, IMapper _mapper) : ControllerBase
    {
        [Authorize]
        [HttpGet("validateToken")]
        public IActionResult ValidateToken() {
            return Ok(new {
                message = "valid"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO) {
            var duplicatedUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == registerDTO.Email);

            if (duplicatedUser != null) return Unauthorized("User already exists");

            try {
                var hmac = new HMACSHA512();

                var user = new UserModel {
                    Name = registerDTO.Name,
                    Email = registerDTO.Email,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                    PasswordSalt = hmac.Key
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                var authDto = _mapper.Map<UserModel, AuthenticatedDTO>(user)!;
                authDto.Token = _handleToken.GenerateToken(user);

                return Ok(authDto);
            } catch(Exception e) {
                return Unauthorized(e.Message);
            }
        }
    
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) {
            UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (user == null) return Unauthorized("Email not found!");

            var hmac = new HMACSHA512(user.PasswordSalt);

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            if (hash.Length != user.PasswordHash.Length) return Unauthorized("Incorrect password");

            for(int i = 0; i < hash.Length; i++) {
                if (hash[i] != user.PasswordHash[i]) return Unauthorized("Incorrect password");
            }

            var authDto = _mapper.Map<UserModel, AuthenticatedDTO>(user)!;
            authDto.Token = _handleToken.GenerateToken(user);

            return Ok(authDto);
        }
    }
}
