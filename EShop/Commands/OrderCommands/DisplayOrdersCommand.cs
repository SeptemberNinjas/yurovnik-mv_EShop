using Core;
using EShop.Pages;

namespace EShop.Commands.OrderCommands
{
    public class DisplayOrdersCommand : ICommandExecutable, IDisplayable
    {
        private readonly List<Order> _orders;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayOrders";

        public string? Result { get; private set; }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать все заказы";
        }
        public DisplayOrdersCommand(List<Order> orders)
        {
            _orders = orders;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            if (_orders is null || _orders.Count == 0)
            {
                Result = "Заказов пока нет:(";
                return;
            }

            Result = string.Join(Environment.NewLine, _orders.Select(item => item.ToString()).ToArray());
            return;
        }

        public void Display()
        {
            Console.WriteLine(GetInfo());
        }
    }
}
