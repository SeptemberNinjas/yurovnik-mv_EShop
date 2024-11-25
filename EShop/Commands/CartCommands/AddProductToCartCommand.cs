using Core;
using DAL;
using EShop.Pages;


namespace EShop.Commands.CartCommands
{
    public class AddProductToCartCommand : ICommandExecutable, IDisplayable
    {
        private IRepository<Product> _productRepo;
        private IRepository<Cart> _cartRepo;

        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "AddProductToCart";

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
            return "Добавить продукт в корзину";
        }

        public AddProductToCartCommand(RepositoryFactory repositoryFactory)
        {
            _cartRepo = repositoryFactory.CreateCartFactory();
            _productRepo = repositoryFactory.CreateProductFactory();
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
                var cart = _cartRepo.GetAll().FirstOrDefault() ?? new Cart();
                var item = _productRepo.GetById(id);
                if (item is null)
                {
                    Result = "Товар не найден";
                    return;
                }
                Result = cart.AddProduct(item, count);
                _cartRepo.Insert(cart);
                return;
            }

            Result = "Не корректный тип параметра";

        }

        public async Task ExecuteAsync(string[]? args)
        {
            if (args is null || args.Length < 2)
            {
                Result = "Не хватает аргументов ";
                return;
            }

            if (int.TryParse(args[0], out var id) && int.TryParse(args[1], out var count))
            {
                var cart = (await _cartRepo.GetAllAsync()).FirstOrDefault() ?? new Cart(); ;                
                var item = await _productRepo.GetByIdAsync(id);
                if (item is null)
                {
                    Result = "Товар не найден";
                    return;
                }
                Result = cart.AddProduct(item, count);
                await _cartRepo.InsertAsync(cart, default);
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

        public async Task DisplayAsync()
        {
            await Task.Run(() => 
            {
                Console.WriteLine(GetInfo());
            });
        }
    }
}
