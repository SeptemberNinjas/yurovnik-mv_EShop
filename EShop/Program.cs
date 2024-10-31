using Core;
using EShop.Data;
using EShop.Commands;
using EShop.Pages;

namespace EShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine(ApplicationContext.Title);
            //Console.WriteLine(App.ExecuteStartupCommand());

           

            var application = new ApplicationContext(100, 50);
            application.StartApp();
        }

        //private static void Execute(string command)
        //{
        //    if (string.IsNullOrEmpty(command))
        //    {
        //        Console.WriteLine("Ошибка: неизвестная команда");
        //        return;
        //    }

        //    var commandNameWithArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //    var commandName = commandNameWithArgs[0];
        //    var args = new string[commandNameWithArgs.Length - 1];
        //    for (var i = 0; i < args.Length; i++)
        //    {
        //        args[i] = commandNameWithArgs[i + 1];
        //    }

        //    Console.WriteLine(App.ExecuteCommandByName(commandName, args));
        //}
    }
}
