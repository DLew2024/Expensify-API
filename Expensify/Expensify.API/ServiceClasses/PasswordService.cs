using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Expensify.API.ServiceClasses
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher = new();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new object(), password);
        }

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                new object(),
                storedPasswordHash,
                enteredPassword
            );

            return result == PasswordVerificationResult.Success
                || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
