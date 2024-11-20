using Core;
using DAL;
using EShop.Pages;
using System.Text;

namespace EShop.Commands.CatalogCommands
{
    public class DisplayServicesCommand : ICommandExecutable, IDisplayable
    {
        private readonly IRepository<Service> _services;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayServices";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; private set; }

        public DisplayServicesCommand(RepositoryFactory repositoryFactory)
        {
            _services = repositoryFactory.CreateServiceFactory();
        }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать услуги";
        }
        /// <summary>
        /// Вывести на экран
        /// </summary>
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
            var sb = new StringBuilder();
            var serives = _services.GetAll();

            if (args is null || args.Length == 0)
            {
                foreach (var item in serives)
                {
                    sb.AppendLine(item.GetDisplayText());
                }
                Result = sb.ToString();
                return;
            }

            if (int.TryParse(args[0], out var count))
            {
                for (int i = 0; i < Math.Min(count, serives.Count); i++)
                {
                    sb.AppendLine(serives.ElementAt(i).GetDisplayText());
                }
                Result = sb.ToString();
                return;
            }
            else
            {
                Result = "Введенный параметр не является числом";
                return;
            }
        }

        private void PrintItems(Service[] items)
        {
            foreach (var item in items)
            {
                Console.WriteLine();
                Console.WriteLine(item.GetDisplayText());
            }
        }
    }
}
