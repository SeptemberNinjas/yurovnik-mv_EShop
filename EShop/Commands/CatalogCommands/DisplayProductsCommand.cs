using Application.Orders;
using Application.SaleItem;
using Core;
using DAL;
using EShop.Pages;
using System.Text;

namespace EShop.Commands.CatalogCommands
{
    public class DisplayProductsCommand : ICommandExecutable, IDisplayable
    {
        private GetSaleItemHandler _productsHandler;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayProducts";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result {get; private set;}

        public DisplayProductsCommand(GetSaleItemHandler getSaleItemHandler)
        {
            _productsHandler = getSaleItemHandler;
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            _ = int.TryParse(args?.FirstOrDefault(), out int count);
            
            var products = await _productsHandler.GetItemsAsync(ItemTypes.Product, count, cancellationToken);

            if (products.IsFailed)
            {
                Result = "Не удалось получить список товаров";
                return;
            }

            var sb = new StringBuilder("Список товаров:").AppendLine();

            for (int i = 0; i < products.Value.Count(); i++)
            {
                var item = products.Value.ElementAt(i);
                sb
                 .Append($"{item.Id}. {item.Name}. Цена: {item.Price:F2}. Остатки: {item.Stock}")
                 .AppendLine();
            }

            Result = sb.ToString();
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
