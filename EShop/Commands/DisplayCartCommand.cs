using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public class DisplayCartCommand
    {
        private Cart _cart;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayCart";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Отобразить корзину покупок";
        }

        public DisplayCartCommand(Cart cart)
        {
            _cart = cart;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public string Execute()
        {
            return _cart.ToString();
        }
    }
}
