using Core;
using EShop.Data;

namespace EShop.Commands
{
    public static class DisplayServicesCommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayServices";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать услуги";
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
                return string.Join(Environment.NewLine, Database.GetServices().Select(item => item.ToString()).ToArray());
            }

            if (int.TryParse(args[0], out var count))
            {
                return string.Join(Environment.NewLine, Database.GetServices(count).Select(item => item.ToString()).ToArray());
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
