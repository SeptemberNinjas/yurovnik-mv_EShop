using Core;
using DAL;
using EShop.Pages;
using System.Text;

namespace EShop.Commands.CatalogCommands
{
    public class DisplayProductsCommand : ICommandExecutable, IDisplayable
    {
        private IRepository<Product> _products;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayProducts";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result {get; private set;}

        public DisplayProductsCommand(RepositoryFactory repositoryFactory)
        {
            _products = repositoryFactory.CreateProductFactory();
        }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать товары";
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
            var products = _products.GetAll();
            if (args is null || args.Length == 0)
            {
                foreach (var item in products)
                {
                    sb.AppendLine(item.GetDisplayText());
                }
                Result = sb.ToString();
                return;
            }

            if (int.TryParse(args[0], out var count))
            {                
                for (int i = 0; i < Math.Min(count, products.Count); i++)
                {
                    sb.AppendLine(products.ElementAt(i).GetDisplayText());
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

        public async Task ExecuteAsync(string[]? args)
        {
            var sb = new StringBuilder();
            var products = await _products.GetAllAsync();
            if (args is null || args.Length == 0)
            {
                foreach (var item in products)
                {
                    sb.AppendLine(item.GetDisplayText());
                }
                Result = sb.ToString();
                return;
            }

            if (int.TryParse(args[0], out var count))
            {
                for (int i = 0; i < Math.Min(count, products.Count); i++)
                {
                    sb.AppendLine(products.ElementAt(i).GetDisplayText());
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

        public async Task DisplayAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine(GetInfo());
            });
        }
    }
}
