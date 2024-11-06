using Core;

namespace DAL.Memory
{
    internal class ProductMemoryRepository : IRepository<Product>
    {
        private readonly List<Product> _products;

        public ProductMemoryRepository()
        {
            _products = 
            [
                new Product(1, "Носки", 12.5m, 50),
                new Product(2, "Макароны", 35, 100),
                new Product(2, "Пуговицы", 15, 230)
            ];
        }
        /// <summary>
        /// Получить все продукты
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Product> GetAll()
        {
            return _products.AsReadOnly();
        }
        /// <summary>
        /// Получить продукт по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }        
        /// <summary>
        /// Получить количество продуктов
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return _products.Count;
        }
        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="product"></param>
        public void Insert(Product product) 
        {
            _products.Add(product);
        }
    }
}
