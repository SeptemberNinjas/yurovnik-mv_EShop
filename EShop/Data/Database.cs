using Core;
using System.Text.Json;

namespace EShop.Data
{
    public static class Database
    {
        private const string ProductsSrc = "D:\\dev\\C#\\EShop\\EShop\\Data\\Products.json";
        private const string ServicesSrc = "D:\\dev\\C#\\EShop\\EShop\\Data\\Services.json";

        public static Product[] GetProducts(int count = 0)
        {
            return GetItems<Product>(count, ProductsSrc);
        }

        public static Service[] GetServices(int count = 0)
        {
            return GetItems<Service>(count, ServicesSrc);
        }

        private static T[] GetItems<T>(int count, string src)
        {
            var result = new T[count];
            var jsonDataBase = File.ReadAllText(src);
            var services = JsonSerializer.Deserialize<T[]>(jsonDataBase);

            if (services is null)
            {
                return result;
            }
            else if (count == 0)
            {
                return services;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    result[i] = services[i];
                }
            }

            return result;
        }
    }
}
