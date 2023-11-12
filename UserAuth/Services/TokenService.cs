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
            new Claim("username", user.Name),
            new Claim(ClaimTypes.DateOfBirth,  user.BornDate.ToString())
        };

        //string secretKey = KeyGenerator.GenerateSecretKey();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GHFUJA84629DHAKCGASIK4720"));

        var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: signingcredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}