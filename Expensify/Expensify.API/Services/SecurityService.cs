using System.Security.Cryptography;
using System.Text;
using Expensify.API.Services.Interfaces;

namespace Expensify.API.Services;

public class SecurityService : ISecurityService
{
    public string GenerateSecureToken(int byteLength = 64)
    {
        var bytes = RandomNumberGenerator.GetBytes(byteLength);

        return Convert.ToBase64String(bytes);
    }

    public string HashToken(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));

        return Convert.ToBase64String(bytes);
    }
}
