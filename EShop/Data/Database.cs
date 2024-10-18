using Core;
using System.Text.Json;

namespace EShop.Data
{
    public static class Database
    {
        private const string ProductsSrc = "Data\\Products.json";
        private const string ServicesSrc = "Data\\Services.json";

        public static Product[] GetProducts(int count = 0)
        {
            return GetItems<Product>(count, ProductsSrc);
        }

        public static Service[] GetServices(int count = 0)
        {
            return GetItems<Service>(count, ServicesSrc);
        }

        public static Product? GetProductById(int id)
        {
            var items = GetItems<Product>(0, ProductsSrc);
            var foundedProduct = items.Where(item => item.Id == id).ToArray();
            if (foundedProduct.Length == 0)
                return null;
            return foundedProduct[0];
        }

        public static Service? GetServiceById(int id)
        {
            var items = GetItems<Service>(0, ServicesSrc);
            var foundedService = items.Where(item => item.Id == id).ToArray();
            if (foundedService.Length == 0)
                return null;
            return foundedService[0];
        }

        private static T[] GetItems<T>(int count, string src)
        {
            var result = new T[count];
            var jsonDataBase = File.ReadAllText(src);
            var items = JsonSerializer.Deserialize<T[]>(jsonDataBase);

            if (items is null)
            {
                return result;
            }
            else if (count == 0)
            {
                return items;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    result[i] = items[i];
                }
            }

            return result;
        }
    }
}
