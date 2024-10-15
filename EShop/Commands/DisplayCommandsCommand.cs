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
            return string.Join("\n", new[] { 
                $"{DisplayCommandsCommand.Name} - {DisplayCommandsCommand.GetInfo()}",
                $"{ExitCommand.Name} - {ExitCommand.GetInfo()}",
                $"{DisplayServicesCommand.Name} - {DisplayServicesCommand.GetInfo()}",
                $"{DisplayProductsCommand.Name} - {DisplayProductsCommand.GetInfo()}"
            });           
        }
    }
}
