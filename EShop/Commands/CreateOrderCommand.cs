using Core;
using EShop.Data;

namespace EShop.Commands
{
    internal class CreateOrderCommand
    {
        private readonly List<Order> _orders;
        public const string Name = "CreateOrder";

        public static string GetInfo()
        {
            return "Создать заказ";
        }
        public CreateOrderCommand(List<Order> orders)
        {
            this._orders = orders;
        }

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