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
            var application = new ApplicationContext(100, 50);
            application.StartApp();           
        }
    }
}
