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
            new Claim("username", user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7DJQ90RGCB0147SBSK"));

        var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: signingcredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}