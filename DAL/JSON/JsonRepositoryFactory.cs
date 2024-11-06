using Core;

namespace DAL.JSON
{
    public class JsonRepositoryFactory : RepositoryFactory
    {
        /// <summary>
        /// Создать JSON репозиторий для продкутов
        /// </summary>
        /// <returns></returns>
        public override IRepository<Product> CreateProductFactory() => new ProductJsonRepository();
        /// <summary>
        /// Создать JSON репозиторий для услуг
        /// </summary>
        /// <returns></returns>
        public override IRepository<Service> CreateServiceFactory() => new ServiceJsonRepository();
        
    }
}
