namespace Expensify.API.ServiceClasses.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string enteredPassword, string storedPasswordHash);
}
