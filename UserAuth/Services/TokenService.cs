using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuth.Models;

namespace UserAuth.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("id", user.Id),
            new Claim("username", user.UserName),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        var keyS = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DUHW92831DINDW0U28BD92DJS9J1"));

        var signingCredentials = new SigningCredentials(keyS, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
            );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}