using EShop.Pages;

namespace EShop.Commands.SystemCommands
{
    public class ExitCommand : ICommandExecutable, IDisplayable
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "Exit";

        /// <summary>
        /// Результат
        /// </summary>
        public string? Result => string.Empty;

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return "Выйти из программы";
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            Environment.Exit(0);
        }

        public void Display()
        {
            Console.WriteLine(GetInfo());
        }

        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Environment.Exit(0);
            });
        }

        public async Task DisplayAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(GetInfo());
            });
        }
    }
}
