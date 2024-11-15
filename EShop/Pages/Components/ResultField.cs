using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Pages.Components
{
    public class ResultField : ConsoleTextBox, IDisplayable
    {
        public ResultField(string text,  ConsoleColor color = ConsoleColor.White) : base(text, color) {}
        /// <summary>
        /// Вывести на экран
        /// </summary>
        public new void Display()
        {
            Console.WriteLine();
            Console.WriteLine("Результат выполнения: ");
            base.Display();
        }
    }
}
