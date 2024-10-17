using Core;
using EShop.Commands;

namespace EShop
{
    public class ApplicationContext
    {

        private Cart _cart = new();
        /// <summary>
        /// Заголовок приложения
        /// </summary>
        public const string Title = "Интернет магазин";

        /// <summary>
        /// Выполнить стартовую команду
        /// </summary>
        /// <returns></returns>
        public string ExecuteStartupCommand()
        {
            return ExecuteCommandByName(DisplayCommandsCommand.Name);
        }

        /// <summary>
        /// Выполнить команду по имени
        /// </summary>
        public string ExecuteCommandByName(string commandName, string[]? args = null)
        {
            return commandName switch
            {
                DisplayCommandsCommand.Name => DisplayCommandsCommand.Execute(),
                ExitCommand.Name => ExitCommand.Execute(),
                DisplayProductsCommand.Name => DisplayProductsCommand.Execute(args),
                DisplayServicesCommand.Name => DisplayServicesCommand.Execute(args),
                DisplayCartCommand.Name => new DisplayCartCommand(_cart).Execute(),
                AddServiceToCartCommand.Name => new AddServiceToCartCommand(_cart).Execute(args),
                AddProductToCartCommand.Name => new AddProductToCartCommand(_cart).Execute(args),
                var _ => "Неизвестаная команда"
            };
        }
    }
}
