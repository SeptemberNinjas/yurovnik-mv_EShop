using EShop.Commands;
using EShop.Commands.CatalogCommands;
using EShop.Commands.SystemCommands;
using EShop.Pages.Components;
using System.Text;

namespace EShop.Pages
{
    public class MainPage : IDisplayable
    {

        private List<IDisplayable> Elements { get; set; }

        public MainPage(List<IDisplayable> elements)
        {
            Elements = elements;
        }

        public void Display()
        {
            foreach (var element in Elements)
            {
                if (element is ResultField resultField && resultField.Text == "")
                {
                    continue;
                }
                Console.WriteLine();
                element.Display();                
            }
        }
    }
}
