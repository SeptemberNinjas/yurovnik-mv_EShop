using Core;
using Core.Payments;
using DAL;
using DAL.Database;
using EShop.Commands;
using EShop.Commands.CartCommands;
using EShop.Commands.CatalogCommands;
using EShop.Commands.OrderCommands;
using EShop.Commands.PaymentCommands;
using EShop.Commands.SystemCommands;
using EShop.Pages;
using EShop.Pages.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EShop
{
    public class ApplicationContext
    {
        private readonly IServiceProvider _serviceProvider;
    
        private readonly List<Order> _orders = new();
        private List<Payment> unpaidPayments= new();
        private MainPage? _mainPage;
        private UsersInput usersInput = new("Введите команду: ");
        private ResultField resultFiled = new("", ConsoleColor.DarkGray);
        private CommandsList commandList;
        private ConsoleTextBox title = new("Интернет магазин", ConsoleColor.Green);

        /// <summary>
        /// Заголовок приложения
        /// </summary>
        public const string Title = "Интернет магазин";

        public ApplicationContext(int width, int height, IConfiguration configuration)
        {
            var services = new ServiceCollection()
                .AddScoped<RepositoryFactory>(sp =>
                {
                    return new DatabaseRepositoryFactory(configuration["ConnectionString"] ?? "");
                })
                .AddScoped<DisplayProductsCommand>()
                .AddScoped<DisplayServicesCommand>()
                .AddScoped<AddProductToCartCommand>()
                .AddScoped<AddServiceToCartCommand>()
                .AddScoped<DisplayCartCommand>()
                .AddScoped<CreateOrderCommand>()
                .AddScoped<DisplayOrdersCommand>()
                .AddScoped<CreatePaymentCommand>()
                .AddScoped<DisplayPaymentsCommand>()
                .AddScoped<MakePaymentCommand>()
                .AddScoped<ExitCommand>()
                .AddScoped<List<Payment>>();

            _serviceProvider = services.BuildServiceProvider();

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
                (IDisplayable)CreateCommand(CommandType.CreatePayment),
                (IDisplayable)CreateCommand(CommandType.MakePayment),
                (IDisplayable)CreateCommand(CommandType.DisplayPayments),
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
            using var scope = _serviceProvider.CreateScope();
            var _repositoryFactory = scope.ServiceProvider.GetRequiredService<RepositoryFactory>();

            return commandType switch
            {
                CommandType.CreatePayment => scope.ServiceProvider.GetRequiredService<CreatePaymentCommand>(),
                CommandType.MakePayment => scope.ServiceProvider.GetRequiredService<MakePaymentCommand>(),
                CommandType.DisplayPayments => scope.ServiceProvider.GetRequiredService<DisplayPaymentsCommand>(),
                CommandType.DisplayCart => scope.ServiceProvider.GetRequiredService<DisplayCartCommand>(),
                CommandType.DisplayServices => scope.ServiceProvider.GetRequiredService<DisplayServicesCommand>(),
                CommandType.DisplayProducts => scope.ServiceProvider.GetRequiredService<DisplayProductsCommand>(),
                CommandType.AddProductToCart => scope.ServiceProvider.GetRequiredService<AddProductToCartCommand>(),
                CommandType.AddServiceToCart => scope.ServiceProvider.GetRequiredService<AddServiceToCartCommand>(),
                CommandType.DisplayOrders => scope.ServiceProvider.GetRequiredService<DisplayOrdersCommand>(),
                CommandType.CreateOrder => scope.ServiceProvider.GetRequiredService<CreateOrderCommand>(),
                CommandType.Exit => scope.ServiceProvider.GetRequiredService<ExitCommand>(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Запустить приложение
        /// </summary>
        public async Task StartApp()
        {      
            var elements = new List<IDisplayable>()
            {
               title,
               commandList,
               resultFiled,
               usersInput,
            };
            _mainPage = new MainPage(elements);
            await LifeCycle();
        }

        private async Task LifeCycle()
        {
            while (true)
            {
                await Draw();
                await Update();
            }
        }

        private async Task Update()
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
            await commnad!.ExecuteAsync(args);
            if (commnad.Result is not null)
            {
                resultFiled.Text = commnad.Result;
            }           
        }

        private async Task Draw()
        {
            Console.Clear();
            await _mainPage?.DisplayAsync();
        }
    }
}
