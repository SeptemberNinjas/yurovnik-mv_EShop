using Core;
using EShop.Data;
using EShop.Pages;

namespace EShop.Commands.CatalogCommands
{
    public class DisplayProductsCommand : ICommandExecutable, IDisplayable
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayProducts";

        public string? Result {get; private set;}

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать товары";
        }

        public void Display()
        {
            Console.WriteLine(GetInfo());
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                Result = string.Join(Environment.NewLine, Database.GetProducts().Select(item => item.GetDisplayText()).ToArray());
                return;
            }

            if (int.TryParse(args[0], out var count))
            {
                Result = string.Join(Environment.NewLine, Database.GetProducts(count).Select(item => item.GetDisplayText()).ToArray());
                return;
            }
            else
            {
                Result = "Введенный параметр не является числом";
                return;
            }
        }
    }
}
