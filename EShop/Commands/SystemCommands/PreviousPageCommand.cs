namespace EShop.Commands.SystemCommands
{
    public class PreviousPageCommand : ICommandExecutable
    {

        public const string Info = "Предыдущая страница";

        public string? Result => string.Empty;

        public void Execute(string[]? args)
        {
            throw new NotImplementedException();
        }
    }
}
