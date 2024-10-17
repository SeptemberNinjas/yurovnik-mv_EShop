using EShop.Data;
using Core;

namespace EShop.Commands
{
    public class AddToCartCommand
    {
        public const string Name = "AddToCart";
        private readonly Cart _cart;

        public static string GetInfo()
        {
            return 
                """
                Добавить товар в корзину
                    AddToCart <Тип> <Id> <Количество>
                Например: AddToCart 1 1 2
                    1 - Service
                """;
        }

        public AddToCartCommand(Cart cart)
        {
            _cart = cart;
        }

        //public static string Execute(string[]? args)
        //{
        //    if (args is null || args.Length < 3)
        //    {
        //        return "Необходимо указать параметры желаемого товара, например: AddToCart  ";
        //    }

        //    if (!int.TryParse(args[0], out var id))
        //    {
        //    }


        //    if (int.TryParse(args[0], out var count))
        //    {
        //    }
        //}
    }
}
