using Core;
using EShop.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands.CartCommands
{
    public class DisplayCartCommand : ICommandExecutable, IDisplayable
    {
        private Cart _cart;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayCart";

        public string? Result { get; private set; }

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
        public void Execute(string[]? args)
        {
            Result = _cart.ToString();
        }

        public void Display()
        {
            Console.WriteLine(GetInfo());
        }
    }
}
