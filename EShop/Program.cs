using Core;
using EShop.Data;
using EShop.Commands;
using EShop.Pages;

namespace EShop
{
    public class Program
    {
        private static readonly ApplicationContext App = new();

        public static void Main(string[] args)
        {
            var mainPage = new MainWindow(ApplicationContext.Title, 100, 50);
            Console.WriteLine(ApplicationContext.Title);
            Console.WriteLine(App.ExecuteStartupCommand());

            while (true)
            {
                Console.WriteLine("Выполните команду");
                var command = Console.ReadLine();
                Execute(command);
            }
        }

        private static void Execute(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine("Ошибка: неизвестная команда");
                return;
            }

            var commandNameWithArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var commandName = commandNameWithArgs[0];
            var args = new string[commandNameWithArgs.Length - 1];
            for (var i = 0; i < args.Length; i++)
            {
                args[i] = commandNameWithArgs[i + 1];
            }

            Console.WriteLine(App.ExecuteCommandByName(commandName, args));
        }
    }
}
