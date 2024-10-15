using EShop.Commands;

namespace EShop
{
    public class ApplicationContext
    {
        public const string Title = "Интернет магазин";

        public string ExecuteStartupCommand()
        {
            return ExecuteCommandByName(DisplayCommandsCommand.Name);
        }

        public string ExecuteCommandByName(string commandName, string[]? args = null)
        {
            return commandName switch
            {
                DisplayCommandsCommand.Name => DisplayCommandsCommand.Execute(),
                ExitCommand.Name => ExitCommand.Execute(),
                DisplayProductsCommand.Name => DisplayProductsCommand.Execute(args),
                DisplayServicesCommand.Name => DisplayServicesCommand.Execute(args),
                var _ => "Неизвестаная команда"
            };
        }
    }
}
