using Core;
using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands.CartCommands
{
    public class AddServiceToCartCommand
    {
        private Cart _cart;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "AddServiceToCart";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Добавить услугу в корзину";
        }

        public AddServiceToCartCommand(Cart cart)
        {
            _cart = cart;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                return "Не хватает аргументов ";
            }

            if (int.TryParse(args[0], out var id))
            {
                var item = Database.GetServiceById(id);
                if (item is null)
                {
                    return "Услуга не найдена";
                }
                return _cart.AddService(item);
            }

            return "Не корректный тип параметра";
        }
    }
}
