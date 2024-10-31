using Core;
using EShop.Data;
using EShop.Pages;


namespace EShop.Commands.CartCommands
{
    public class AddProductToCartCommand : ICommandExecutable, IDisplayable
    {
        private Cart _cart;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "AddProductToCart";

        public string? Result { get; private set; }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Добавить продукт в корзину";
        }

        public AddProductToCartCommand(Cart cart)
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
            if (args is null || args.Length < 2)
            {
                Result = "Не хватает аргументов ";
                return;
            }

            if (int.TryParse(args[0], out var id) && int.TryParse(args[1], out var count))
            {
                var item = Database.GetProductById(id);
                if (item is null)
                {
                    Result = "Товар не найден";
                    return;
                }
                Result = _cart.AddProduct(item, count);
                return;
            }

            Result = "Не корректный тип параметра";

        }

        public void Display()
        {
            Console.WriteLine(GetInfo());
        }
    }
}
