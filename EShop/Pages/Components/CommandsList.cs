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
        public List<IDisplayable> Commands { get; init; }

        public CommandsList(List<IDisplayable> commands)
        {
            Commands = commands;
        }

        public void Display()
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                Commands[i].Display();
            }
        }
    }
}
