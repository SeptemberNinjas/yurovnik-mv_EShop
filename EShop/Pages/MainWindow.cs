using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Pages
{
    public class MainWindow
    {
        public MainWindow(string title, int width, int height)
        {
            Console.Title = title;
            Console.SetWindowSize(width, height);
        }
    }
}
