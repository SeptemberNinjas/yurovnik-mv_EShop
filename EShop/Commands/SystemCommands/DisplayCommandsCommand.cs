using EShop.Commands.CartCommands;
using EShop.Commands.CatalogCommands;
using EShop.Commands.OrderCommands;

namespace EShop.Commands.SystemCommands
{
    public static class DisplayCommandsCommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public const string Name = "DisplayCommands";

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Вывести список команд";
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <returns></returns>
        public static string Execute()
        {
            return string.Join(Environment.NewLine, new[] {
                $"{Name} - {GetInfo()}",
                $"{ExitCommand.Name} - {ExitCommand.GetInfo()}",
                $"{DisplayServicesCommand.Name} - {DisplayServicesCommand.GetInfo()}",
                $"{DisplayProductsCommand.Name} - {DisplayProductsCommand.GetInfo()}",
                $"{AddProductToCartCommand.Name} - {AddProductToCartCommand.GetInfo()}",
                $"{AddServiceToCartCommand.Name} - {AddServiceToCartCommand.GetInfo()}",
                $"{DisplayCartCommand.Name} - {DisplayCartCommand.GetInfo()}",
                $"{CreateOrderCommand.Name} - {CreateOrderCommand.GetInfo()}",
                $"{DisplayOrdersCommand.Name} - {DisplayOrdersCommand.GetInfo()}"
            });
        }
    }
}
