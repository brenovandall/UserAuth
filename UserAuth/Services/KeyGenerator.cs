using System.Security.Cryptography;

namespace UserAuth.Services;

public class KeyGenerator
{
    public static string GenerateSecretKey(int length = 32)
    {
        byte[] randomBytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }
}
