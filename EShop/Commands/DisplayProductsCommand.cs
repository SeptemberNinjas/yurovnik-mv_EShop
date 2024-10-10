using Core;
using EShop.Data;

namespace EShop.Commands
{
    public static class DisplayProductsCommand
    {
        public const string Name = "DisplayProducts";

        public static string GetInfo()
        {
            return "Показать товары";
        }

        public static void Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                PrintItems(Database.GetProducts());
                return;
            }

            if (int.TryParse(args[0], out var count))
            {
                PrintItems(Database.GetProducts(count));
            }
            else
            {
                Console.WriteLine("Введенный параметр не является числом");
            }
        }

        private static void PrintItems(Product[] items)
        {
            foreach (var item in items)
            {
                Console.WriteLine();
                Console.WriteLine(item.ToString());
            }
        }
    }
}
