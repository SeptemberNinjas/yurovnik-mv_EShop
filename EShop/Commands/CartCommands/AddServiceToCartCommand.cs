using Core;
using DAL;
using EShop.Pages;


namespace EShop.Commands.CartCommands
{
    public class AddServiceToCartCommand : ICommandExecutable, IDisplayable
    {
        private IRepository<Service> _serviceRepo;
        private IRepository<Cart> _cartRepo;

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

        public AddServiceToCartCommand(RepositoryFactory repositoryFactory)
        {
            _cartRepo = repositoryFactory.CreateCartFactory();
            _serviceRepo = repositoryFactory.CreateServiceFactory();
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
                var cart = _cartRepo.GetAll().FirstOrDefault() ?? new Cart();
                var item = _serviceRepo.GetById(id);
                if (item is null)
                {
                    Result = "Услуга не найдена";
                    return;
                }
                Result = cart.AddService(item);
                _cartRepo.Insert(cart);
                return;
            }

            Result = "Не корректный тип параметра";
        }

        public async Task ExecuteAsync(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                Result = "Не хватает аргументов ";
                return;
            }

            if (int.TryParse(args[0], out var id))
            {
                var cart = (await _cartRepo.GetAllAsync()).FirstOrDefault() ?? new Cart();
                var item = await _serviceRepo.GetByIdAsync(id);
                if (item is null)
                {
                    Result = "Услуга не найдена";
                    return;
                }
                Result = cart.AddService(item);
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
