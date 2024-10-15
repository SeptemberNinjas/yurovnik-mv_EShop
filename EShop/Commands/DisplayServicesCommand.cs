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

        public static string Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                return string.Join("\n", Database.GetServices().Select(item => item.ToString()).ToArray());
            }

            if (int.TryParse(args[0], out var count))
            {
                return string.Join("\n", Database.GetServices(count).Select(item => item.ToString()).ToArray());
            }
            else
            {
                return "Введенный параметр не является числом";
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
