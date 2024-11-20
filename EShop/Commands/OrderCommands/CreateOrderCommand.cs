using Core;
using DAL;
using EShop.Pages;

namespace EShop.Commands.OrderCommands
{
    internal class CreateOrderCommand : ICommandExecutable, IDisplayable
    {
        private IRepository<Order> _orders;
        private IRepository<Cart> _cartRepo;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "CreateOrder";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result {get; private set;}

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Создать заказ";
        }
        public CreateOrderCommand(RepositoryFactory repositoryFactory)
        {
            _orders = repositoryFactory.CreateOrderFactory();
            _cartRepo = repositoryFactory.CreateCartFactory();
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            var cart = _cartRepo.GetAll().FirstOrDefault() ?? new Cart();
            if (cart == null || cart.Count == 0)
            {
                Result = "Невозможно создать заказ. Корзина пуста";
                return;
            }

            _orders.Insert(cart.CreateOrderFromCart());          

            Result = "Заказ успешно создан";
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