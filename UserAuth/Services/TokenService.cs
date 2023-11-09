using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuth.Models;

namespace UserAuth.Services;

public class TokenService
{
    public string GenerateToken(User? user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("id", user.Id),
            new Claim("username", user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("h527dndfajdf8924gnshfkav8bdjd"));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
            );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}