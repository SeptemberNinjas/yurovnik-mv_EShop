using Core;
using EShop.Pages;
using Application.Orders;

namespace EShop.Commands.CartCommands
{
    public class DisplayCartCommand : ICommandExecutable, IDisplayable
    {
        private GetCartHandler _cartHandler;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayCart";
        /// <summary>
        /// Реультат
        /// </summary>
        public string? Result { get; private set; }

        public DisplayCartCommand(GetCartHandler getCartHandler)
        {
            _cartHandler = getCartHandler;
        }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Отобразить корзину покупок";
        }

        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            var result = (await _cartHandler.GetCartAsync(cancellationToken));
            Result = result.Value.ToString();
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
