using Core;
using EShop.Data;

namespace EShop.Commands
{
    public static class DisplayServicesCommand
    {
        public const string Name = "DisplayServices";

        public static string GetInfo()
        {
            return "Показать услуги";
        }

        public static void Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                PrintItems(Database.GetServices());
                return;
            }

            if (int.TryParse(args[0], out var count))
            {
                PrintItems(Database.GetServices(count));
            }
            else
            {
                Console.WriteLine("Введенный параметр не является числом");
            }
        }

        private static void PrintItems(Service[] items)
        {
            foreach (var item in items)
            {
                Console.WriteLine();
                Console.WriteLine(item.ToString());
            }
        }
    }
}
