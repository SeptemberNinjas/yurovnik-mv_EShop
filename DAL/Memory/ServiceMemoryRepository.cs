using Core;

namespace DAL.Memory
{
    public class ServiceMemoryRepository : IRepository<Service>
    {
        private readonly List<Service> _services;

        public ServiceMemoryRepository()
        {
            _services =
            [
                new Service(1, "Доставка", 250),
                new Service(1, "Индивидуальный заказ", 1000)
            ];
        }
        /// <summary>
        /// Получить все услуги
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Service> GetAll()
        {
            return _services.AsReadOnly();
        }
        /// <summary>
        /// Получить услугу по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Service? GetById(int id)
        {
            return _services.FirstOrDefault(p => p.Id == id);
        }
        /// <summary>
        /// Получить количество услуг
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return _services.Count;
        }
        /// <summary>
        /// Добавить услугу
        /// </summary>
        /// <param name="service"></param>
        public void Insert(Service service)
        {
            _services.Add(service);
        }
    }
}
