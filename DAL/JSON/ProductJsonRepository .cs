using Core;
using System.Text.Json;

namespace DAL.JSON
{
    public class ProductJsonRepository : IRepository<Product>
    {
        private const string _productsSrc = "Data\\Products.json";
        /// <summary>
        /// Получить все элементы
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Product> GetAll()
        {
            return (IReadOnlyCollection<Product>)GetProducts();
        }

        /// <summary>
        /// Получить продукт по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product? GetById(int id)
        {
            var products = GetProducts();
            return products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Получить количество
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            var products = GetProducts();
            return products.Count();
        }
        /// <summary>
        /// Добавить продукт в базу
        /// </summary>
        /// <param name="product"></param>
        public void Insert(Product product) 
        {
            var products = GetProducts();
            products.Append(product);

            using var writer = new StreamWriter(_productsSrc, false);
            JsonSerializer.Serialize(writer.BaseStream, products);
        }

        private static IEnumerable<Product> GetProducts()
        {
            if (!File.Exists(_productsSrc))
            {
                using var stream = new StreamWriter(_productsSrc);
                stream.WriteLine("[]");
            }

            using var reader = new StreamReader(_productsSrc);
            var response = JsonSerializer.Deserialize<IEnumerable<Product>>(reader.BaseStream);
            return (IReadOnlyCollection<Product>)(response ?? []);
        }
    }
}
