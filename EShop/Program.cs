using Microsoft.Extensions.Configuration;

namespace EShop
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var confBuilder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false)
               .Build();
            var application = new ApplicationContext(100, 50, confBuilder);
            await application.StartApp();           
        }
    }
}
