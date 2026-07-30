namespace Expensify.API.ServiceClasses.Interfaces;

public interface ISecurityService
{
    string GenerateSecureToken(int byteLength = 64);

    string HashToken(string value);
}
