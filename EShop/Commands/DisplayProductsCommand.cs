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

        public static string Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {            
                return string.Join("\n", Database.GetProducts().Select(item => item.ToString()).ToArray());
            }

            if (int.TryParse(args[0], out var count))
            {
                return string.Join("\n", Database.GetProducts(count).Select(item => item.ToString()).ToArray());
            }
            else
            {
                return "Введенный параметр не является числом";
            }
        }
    }
}
