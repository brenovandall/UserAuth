using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuth.Models;

namespace UserAuth.Services;

public class TokenService
{
    public IConfiguration _configuration;
    // in this project, i have secrets json, so my security key for token is being stored for info security 
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration; // can access the SymmetricSecurityKey secret that i used in Program.cs
    }

    public string GenerateToken(User user)
    {
        
        Claim[] claims = new Claim[]
        {
            new Claim("id", user.Id), // the user id will be encoded
            new Claim("username", user.Name), // the user name will be encoded
            new Claim(ClaimTypes.DateOfBirth,  user.BornDate.ToString()) // the user date of birth will be encoded just for doing authentication for access
        };

        //string secretKey = KeyGenerator.GenerateSecretKey();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

        var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // generating tokens with Jwt extension, the website that we all can test the info veracity is: "https://jwt.io/"
        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: signingcredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token); // converting to string, just because we want to see the token reponse at postman requisition
    }
}