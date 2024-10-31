using Core;
using EShop.Commands;
using EShop.Commands.CartCommands;
using EShop.Commands.CatalogCommands;
using EShop.Commands.OrderCommands;
using EShop.Commands.SystemCommands;
using EShop.Pages;
using EShop.Pages.Components;
using System.Xml.Linq;

namespace EShop
{
    public class ApplicationContext
    {

        private readonly List<Order> _orders = new();
        private readonly Cart _cart = new();
        private MainPage? _mainPage;
        private UsersInput usersInput = new("Введите команду: ");
        private ResultField resultFiled = new("", ConsoleColor.DarkGray);
        private CommandsList commandList;
        private ConsoleTextBox title = new("Интернет магазин", ConsoleColor.Green);

        /// <summary>
        /// Заголовок приложения
        /// </summary>
        public const string Title = "Интернет магазин";

        public ApplicationContext(int width, int height)
        {
            Console.SetWindowSize(width, height);

            commandList = new CommandsList(new List<IDisplayable>()
            {
                (IDisplayable)CreateCommand(CommandType.DisplayProducts),
                (IDisplayable)CreateCommand(CommandType.DisplayServices),
                (IDisplayable)CreateCommand(CommandType.AddProductToCart),
                (IDisplayable)CreateCommand(CommandType.AddServiceToCart),
                (IDisplayable)CreateCommand(CommandType.DisplayCart),
                (IDisplayable)CreateCommand(CommandType.DisplayOrders),
                (IDisplayable)CreateCommand(CommandType.CreateOrder),
                (IDisplayable)CreateCommand(CommandType.Exit),
            });

        }

        /// <summary>
        /// Создать команду
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public ICommandExecutable CreateCommand(CommandType commandType)
        {
            return commandType switch
            {
                CommandType.DisplayCart => new DisplayCartCommand(_cart),
                CommandType.DisplayServices => new DisplayServicesCommand(),
                CommandType.DisplayProducts => new DisplayProductsCommand(),
                CommandType.AddProductToCart => new AddProductToCartCommand(_cart),
                CommandType.AddServiceToCart => new AddServiceToCartCommand(_cart),
                CommandType.DisplayOrders => new DisplayOrdersCommand(_orders),
                CommandType.CreateOrder => new CreateOrderCommand(_orders, _cart),
                CommandType.Exit => new ExitCommand(),
                _ => throw new NotSupportedException()
            };
        }

        public void StartApp()
        {      
            var elements = new List<IDisplayable>()
            {
               title,
               commandList,
               resultFiled,
               usersInput,
            };
            _mainPage = new MainPage(elements);
            LifeCycle();
        }

        private void LifeCycle()
        {
            while (true)
            {
                Draw();
                Update();
            }
        }

        private void Update()
        {
            var input = usersInput.Input;

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ошибка: неизвестная команда");
                return;
            }

            var commandNameWithArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var commandName = commandNameWithArgs[0];
            var args = new string[commandNameWithArgs.Length - 1];
            for (var i = 0; i < args.Length; i++)
            {
                args[i] = commandNameWithArgs[i + 1];
            }

            if (!int.TryParse(commandName, out var commandNumber) || commandNumber > commandList?.Commands.Count)
            {
                Console.WriteLine("Неизвестная команда");
                return;
            }

            var commnad = commandList!.Commands[commandNumber - 1] as ICommandExecutable;
            commnad!.Execute(args);
            if (commnad.Result is not null)
            {
                resultFiled.Text = commnad.Result;
            }           
        }

        private void Draw()
        {
            Console.Clear();
            _mainPage?.Display();
        }
    }
}
