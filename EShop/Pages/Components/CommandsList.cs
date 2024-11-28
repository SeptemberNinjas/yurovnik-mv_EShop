using EShop.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Pages.Components
{
    public class CommandsList : IDisplayable
    {
        /// <summary>
        /// Список команд
        /// </summary>
        public List<IDisplayable> Commands { get; init; }

        public CommandsList(List<IDisplayable> commands)
        {
            Commands = commands;
        }
        /// <summary>
        /// Вывести на экран
        /// </summary>
        public void Display()
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                Commands[i].Display();
            }
        }

        public async Task DisplayAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                await Commands[i].DisplayAsync(default);
            }
        }
    }
}
