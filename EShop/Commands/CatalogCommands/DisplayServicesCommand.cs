using Application.SaleItem;
using Core;
using DAL;
using EShop.Pages;
using System.Text;

namespace EShop.Commands.CatalogCommands
{
    public class DisplayServicesCommand : ICommandExecutable, IDisplayable
    {
        private readonly GetSaleItemHandler _servicesHandler;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayServices";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; private set; }

        public DisplayServicesCommand(GetSaleItemHandler getSaleItemHandler)
        {
            _servicesHandler = getSaleItemHandler;
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

        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            _ = int.TryParse(args?.ToString(), out int count);

            var services = await _servicesHandler.GetItemsAsync(ItemTypes.Service, count, cancellationToken);

            if (services.IsFailed)
            {
                Result = "Не удалось получить список услуг";
                return;
            }

            var sb = new StringBuilder();

            for (int i = 0; i < services.Value.Count(); i++)
            {
                var item = services.Value.ElementAt(i);
                sb
                 .Append($"{item.Id}. {item.Name}. Цена: {item.Price:F2}")
                 .AppendLine();
            }

            Result = sb.ToString();
        }

        private void PrintItems(Service[] items)
        {
            foreach (var item in items)
            {
                Console.WriteLine();
                Console.WriteLine(item.GetDisplayText());
            }
        }

        public async Task DisplayAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(GetInfo());
            });
        }
    }
}
