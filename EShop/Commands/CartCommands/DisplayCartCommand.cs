using Core;
using DAL;
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
        private IRepository<Cart> _cart;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayCart";
        /// <summary>
        /// Реультат
        /// </summary>
        public string? Result { get; private set; }

        public DisplayCartCommand(RepositoryFactory repositoryFactory)
        {
            _cart = repositoryFactory.CreateCartFactory();
        }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Отобразить корзину покупок";
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            var cart = _cart.GetAll().FirstOrDefault() ?? new Cart();
            Result = cart.ToString();
        }

        public async Task ExecuteAsync(string[]? args)
        {
            var cart = (await _cart.GetAllAsync()).FirstOrDefault() ?? new Cart();
            Result = cart.ToString();
        }
        /// <summary>
        /// Вывести на экран
        /// </summary>
        public void Display()
        {
            Console.WriteLine(GetInfo());
        }

        public async Task DisplayAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine(GetInfo());
            });
        }
    }
}
