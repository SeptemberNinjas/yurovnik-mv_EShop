namespace EShop.Commands
{
    public static class DisplayCommandsCommand
    {
        public const string Name = "DisplayCommands";

        public static string GetInfo()
        {
            return "Вывести список команд";
        }

        public static string Execute()
        {
            return string.Join(Environment.NewLine, new[] { 
                $"{DisplayCommandsCommand.Name} - {DisplayCommandsCommand.GetInfo()}",
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
