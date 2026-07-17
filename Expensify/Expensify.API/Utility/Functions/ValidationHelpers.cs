using System.Net.Mail;

namespace Expensify.API.Utility.Functions
{
    public class ValidationHelpers
    {
        public const int PasswordLengthMinimum = 8;

        /// <summary>
        /// Determines whether any of the provided strings are null, empty, or contain only whitespace.
        /// </summary>
        public static bool HasEmptyOrWhiteSpace(params string?[] values)
        {
            return values.Any(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// Determines whether the provided email address is in a valid format.
        /// </summary>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var trimmedEmail = email.Trim();

            try
            {
                var address = new MailAddress(trimmedEmail);

                return address.Address.Equals(trimmedEmail, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes leading and trailing whitespace from the specified string.
        /// Returns an empty string if the value is null.
        /// </summary>
        /// <param name="value">The string to trim.</param>
        /// <returns>The trimmed string, or an empty string if the input is null.</returns>
        public static string Trim(string? value)
        {
            return value?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Normalizes a string for case-insensitive comparisons by trimming
        /// leading and trailing whitespace and converting it to lowercase
        /// using the invariant culture.
        /// Returns an empty string if the value is null.
        /// </summary>
        /// <param name="value">The string to normalize.</param>
        /// <returns>The normalized string, or an empty string if the input is null.</returns>
        public static string Normalize(string? value)
        {
            return value?.Trim().ToLowerInvariant() ?? string.Empty;
        }
    }
}
