using Core;

namespace DAL
{
    public abstract class RepositoryFactory
    {
        public abstract IRepository<Product> CreateProductFactory();
        public abstract IRepository<Service> CreateServiceFactory();

        public abstract IRepository<Cart> CreateCartFactory();

        public abstract IRepository<Order> CreateOrderFactory();

        public abstract IReadOnlyRepository<SaleItem> CreateSaleItemFactory();
    }
}
