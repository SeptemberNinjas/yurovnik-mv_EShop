using Core;
using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public class AddServiceToCartCommand
    {
        private Cart _cart;
        public const string Name = "AddServiceToCart";

        public static string GetInfo()
        {
            return "Добавить услугу в корзину";
        }

        public AddServiceToCartCommand(Cart cart)
        {
            _cart = cart;
        }

        public string Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                return "Не хватает аргументов ";
            }

            if (int.TryParse(args[0], out var id) && int.TryParse(args[1], out var count))
            {
                var item = Database.GetServiceById(id);
                if (item is null)
                {
                    return "Услуга не найдена";
                }
                return _cart.AddLine(item);
            }

            return "Не корректный тип параметра";
        }
    }
}
