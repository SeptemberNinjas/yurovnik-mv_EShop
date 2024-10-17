using Core;
using EShop.Data;
     

namespace EShop.Commands
{
    public class AddProductToCartCommand
    {
        private Cart _cart;
        public const string Name = "AddProductToCart";

        public static string GetInfo()
        {
            return "Добавить продукт в корзину";
        }

        public AddProductToCartCommand(Cart cart)
        {
            _cart = cart;
        }

        public string Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
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
                return _cart.AddLine(item, count);
            }

            return "Не корректный тип параметра";
            
        }
    }
}
