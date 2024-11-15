using Core;

namespace DAL
{
    public abstract class RepositoryFactory
    {
        public abstract IRepository<Product> CreateProductFactory();
        public abstract IRepository<Service> CreateServiceFactory();
    }
}
