using Core;
using EShop.Data;

namespace EShop.Commands
{
    internal class CreateOrderCommand
    {
        private readonly List<Order> _orders;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "CreateOrder";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Создать заказ";
        }
        public CreateOrderCommand(List<Order> orders)
        {
            this._orders = orders;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public string Execute(Cart cart)
        {
            if (cart == null || cart.Count == 0)
            {
                return "Невозможно создать заказ. Корзина пуста";
            }

            _orders.Add(cart.CreateOrderFromCart());

            return "Заказ успешно создан";
        }
    }
}