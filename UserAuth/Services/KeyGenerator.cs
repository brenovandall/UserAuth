using System.Security.Cryptography;

namespace UserAuth.Services;

public class KeyGenerator
{

    // this is the key generator that i made for info security, im not using that at this moment, but its nice to see and
    // understand how works
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
