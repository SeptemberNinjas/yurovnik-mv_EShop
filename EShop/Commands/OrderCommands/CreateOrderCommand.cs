using Core;
using EShop.Data;
using EShop.Pages;

namespace EShop.Commands.OrderCommands
{
    internal class CreateOrderCommand : ICommandExecutable, IDisplayable
    {
        private readonly List<Order> _orders;
        private readonly Cart _cart;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "CreateOrder";

        public string? Result {get; private set;}

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Создать заказ";
        }
        public CreateOrderCommand(List<Order> orders, Cart cart)
        {
            _orders = orders;
            _cart = cart;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            if (_cart == null || _cart.Count == 0)
            {
                Result = "Невозможно создать заказ. Корзина пуста";
                return;
            }

            _orders.Add(_cart.CreateOrderFromCart());

            Result = "Заказ успешно создан";
            return;
        }

        public void Display()
        {
            Console.WriteLine(GetInfo());
        }
    }
}