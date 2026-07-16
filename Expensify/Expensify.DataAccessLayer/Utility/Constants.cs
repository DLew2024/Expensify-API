namespace Expensify.DataAccessLayer.Utility;

public static class Constants
{
    public static Guid EmptyGuid = new Guid("00000000-0000-0000-0000-000000000000");

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
    }
}
