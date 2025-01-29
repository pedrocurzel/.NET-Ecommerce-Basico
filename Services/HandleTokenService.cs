using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _net.Interfaces;
using _net.Models;
using Microsoft.IdentityModel.Tokens;

namespace _net.Services;

public class HandleTokenService(IConfiguration configuration) : IHandleToken
{

    const int MINIMAL_KEY_LENGTH = 64;

    public string GenerateToken(UserModel user)
    {
         var tokenKey = configuration["TokenKey"] ?? throw new Exception("Token Key not found in appsettings.json");

        if (tokenKey.Length < MINIMAL_KEY_LENGTH) throw new Exception("Token Key too small");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Name)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }
}
