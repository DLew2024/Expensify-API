using Microsoft.AspNetCore.Identity;

namespace Expensify.API.Utility.Functions
{
    public static class PasswordValidation
    {
        private static readonly PasswordHasher<object> _passwordHasher = new();

        public static bool ValidatePassword(string enteredPassword, string storedPasswordHash)
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
