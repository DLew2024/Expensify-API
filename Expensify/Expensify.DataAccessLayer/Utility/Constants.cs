namespace Expensify.DataAccessLayer.Utility;

public static class Constants
{
    public static Guid EmptyGuid = new Guid("00000000-0000-0000-0000-000000000000");

    public static class RoleIds
    {
        public static readonly Guid User = new("7E04D972-5CDE-4156-8AC4-B5659549236B");

        public static readonly Guid Admin = new("AA911248-931B-4CB2-9A65-451081AA3976");
    }

    public static class RoleNames
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }

    public static class DatabaseLengths
    {
        public const int Name = 100;
        public const int FullName = 200;
        public const int Email = 320;
        public const int PasswordHash = 500;
        public const int Url = 2048;
        public const int InstitutionName = 100;
        public const int Description = 500;
        public const int Notes = 500;
        public const int LastFourDigits = 4;
        public const int TokenHash = 128;
        public const int RoleName = 20;
        public const int CurrencySymbol = 10;
        public const int CurrencyCode = 3;
    }
}
