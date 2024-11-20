using Microsoft.Extensions.Configuration;

namespace EShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var confBuilder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false)
               .Build();
            var application = new ApplicationContext(100, 50, confBuilder);
            application.StartApp();           
        }
    }
}
