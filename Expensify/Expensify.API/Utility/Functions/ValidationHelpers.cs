namespace Expensify.API.Utility.Functions
{
    public class ValidationHelpers
    {
        public static bool HasEmptyOrWhiteSpace(params string?[] values)
        {
            return values.Any(string.IsNullOrWhiteSpace);
        }
    }
}
