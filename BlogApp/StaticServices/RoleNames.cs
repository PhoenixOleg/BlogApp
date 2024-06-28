namespace BlogApp.StaticServices
{
    public static class RoleNames
    {
        public const string Administrator = "Администратор";
        public const string User = "Пользователь";
        public const string Moderator = "Модератор";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Administrator;
                yield return User;
                yield return Moderator;
            }
        }
    }
}
