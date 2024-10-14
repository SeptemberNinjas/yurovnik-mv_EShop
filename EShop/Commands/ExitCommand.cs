namespace EShop.Commands
{
    public static class ExitCommand
    {
        public const string Name = "Exit";

        public static string GetInfo()
        {
            return "Выйти из прогараммы";
        }

        public static void Execute()
        {
            Environment.Exit(0);
        }
    }
}
