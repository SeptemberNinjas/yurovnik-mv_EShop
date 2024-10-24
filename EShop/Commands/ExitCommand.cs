namespace EShop.Commands
{
    public static class ExitCommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "Exit";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Выйти из прогараммы";
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public static string Execute()
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
