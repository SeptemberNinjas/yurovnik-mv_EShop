using Core;
using System.Text.Json;

namespace EShop.Data
{
    public static class Database
    {
        private const string ProductsSrc = "Data\\Products.json";
        private const string ServicesSrc = "Data\\Services.json";

        /// <summary>
        /// Получить товары
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Product[] GetProducts(int count = 0)
        {
            return GetItems<Product>(count, ProductsSrc);
        }

        /// <summary>
        /// Получить услуги
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Service[] GetServices(int count = 0)
        {
            return GetItems<Service>(count, ServicesSrc);
        }

        /// <summary>
        /// Получить продукт по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Product? GetProductById(int id)
        {
            var items = GetItems<Product>(0, ProductsSrc);
            return items.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        /// Получить услугу по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Service? GetServiceById(int id)
        {
            var items = GetItems<Service>(0, ServicesSrc);
            return items.FirstOrDefault(item => item.Id == id);
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
