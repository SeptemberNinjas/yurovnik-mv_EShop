using Application.Orders;
using Core;
using DAL;
using EShop.Pages;

namespace EShop.Commands.CartCommands
{
    public class AddServiceToCartCommand : ICommandExecutable, IDisplayable
    {
        private AddCartLineHandler _addCartLineHandler;

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

        public AddServiceToCartCommand(AddCartLineHandler addCartLineHandler)
        {
            _addCartLineHandler = addCartLineHandler;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            if (args is null || args.Length == 0)
            {
                Result = "Не хватает аргументов ";
                return;
            }

            if (int.TryParse(args[0], out var id))
            {
                var result = await _addCartLineHandler.AddLineAsync(id, 1, cancellationToken);
                Result = result.ToString();
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

        public async Task DisplayAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(GetInfo());
            });
        }
    }
}
