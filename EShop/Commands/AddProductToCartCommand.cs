using Core;
using EShop.Data;
     

namespace EShop.Commands
{
    public class AddProductToCartCommand
    {
        private Cart _cart;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "AddProductToCart";

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
        public string Execute(string[]? args)
        {
            if (args is null || args.Length < 2)
            {
                return "Не хватает аргументов ";
            }

            if (int.TryParse(args[0], out var id) && int.TryParse(args[1], out var count)) 
            {
                var item = Database.GetProductById(id);
                if (item is null)
                {
                    return "Товар не найден";
                }
                return _cart.AddProduct(item, count);
            }

            return "Не корректный тип параметра";
            
        }
    }
}
