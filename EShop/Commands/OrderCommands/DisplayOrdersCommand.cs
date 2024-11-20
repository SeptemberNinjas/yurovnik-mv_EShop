using Core;
using DAL;
using EShop.Pages;

namespace EShop.Commands.OrderCommands
{
    public class DisplayOrdersCommand : ICommandExecutable, IDisplayable
    {
        private IRepository<Order> _orders;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayOrders";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; private set; }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать все заказы";
        }
        public DisplayOrdersCommand(RepositoryFactory repositoryFactory)
        {
            _orders = repositoryFactory.CreateOrderFactory();
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            if (_orders is null || _orders.GetCount() == 0)
            {
                Result = "Заказов пока нет:(";
                return;
            }

            Result = string.Join(Environment.NewLine, _orders.GetAll().Select(item => item.ToString()).ToArray());
            return;
        }

        /// <summary>
        /// Вывести на экран
        /// </summary>
        public void Display()
        {
            Console.WriteLine(GetInfo());
        }
    }
}
