using Core;
using EShop.Data;
using EShop.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands.CartCommands
{
    public class AddServiceToCartCommand : ICommandExecutable, IDisplayable
    {
        private Cart _cart;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "AddServiceToCart";

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
        public void Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                Result = "Не хватает аргументов ";
                return;
            }

            if (int.TryParse(args[0], out var id))
            {
                var item = Database.GetServiceById(id);
                if (item is null)
                {
                    Result = "Услуга не найдена";
                    return;
                }
                Result = _cart.AddService(item);
                return;
            }

            Result = "Не корректный тип параметра";
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
