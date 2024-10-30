using Core;
using EShop.Data;

namespace EShop.Commands.CatalogCommands
{
    public static class DisplayProductsCommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayProducts";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать товары";
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                return string.Join(Environment.NewLine, Database.GetProducts().Select(item => item.ToString()).ToArray());
            }

            if (int.TryParse(args[0], out var count))
            {
                return string.Join(Environment.NewLine, Database.GetProducts(count).Select(item => item.ToString()).ToArray());
            }
            else
            {
                return "Введенный параметр не является числом";
            }
        }
    }
}
