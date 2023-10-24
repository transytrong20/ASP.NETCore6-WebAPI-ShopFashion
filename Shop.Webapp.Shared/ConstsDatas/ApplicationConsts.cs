namespace Shop.Webapp.Shared.ConstsDatas
{
    public static class ApplicationConsts
    {
        public const string DefaultWebName = "shop";
        public const string Schema = "shop";
        public const string FirstRoute = "shop/data";

        public const string AvatarDefault = "/commons/avatar.png";

        public static readonly string[] NumberOTPKey = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static readonly string[] CharOTPKey = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        public const string UniqueClientKey = "ClientTokenId";

        public const int DefaultPetCountOnMonth = 2;
        public const int DefaultMaxService = 2;
        public const int DefaultMaxProduct = 2;
    }
}
