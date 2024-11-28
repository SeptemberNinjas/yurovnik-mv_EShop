using Application.Orders;
using Core;
using DAL;
using EShop.Pages;

namespace EShop.Commands.OrderCommands
{
    internal class CreateOrderCommand : ICommandExecutable, IDisplayable
    {
        private CreateOrderHandler _createOrderHandler;
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "CreateOrder";
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result {get; private set;}

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Создать заказ";
        }
        public CreateOrderCommand(CreateOrderHandler createOrderHandler)
        {
            _createOrderHandler = createOrderHandler;
        }

        /// <summary>
        /// Выполнить команду асинхронно
        /// </summary>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            var result = await _createOrderHandler.CreateOrderAsync(cancellationToken);
            Result = result.ToString();
            return;
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