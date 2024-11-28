using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database
{
    public class DatabaseRepositoryFactory : RepositoryFactory
    {
        private readonly string _connectionString;

        public DatabaseRepositoryFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override IRepository<Cart> CreateCartFactory()
        {
            return new CartDatabaseRepository(_connectionString);
        }

        public override IRepository<Order> CreateOrderFactory()
        {
            return new OrderDatabaseRepository(_connectionString);
        }

        public override IRepository<Product> CreateProductFactory()
        {
            return new ProductDatabaseRepository(_connectionString);
        }

        public override IReadOnlyRepository<SaleItem> CreateSaleItemFactory()
        {
            return new SaleItemDatabaseRepository(_connectionString);
        }

        public override IRepository<Service> CreateServiceFactory()
        {
            return new ServiceDatabaseRepository(_connectionString);
        }

        public override IRepository<Stock> CreateStockFactory()
        {
            return new StockDatabaseRepository(_connectionString);
        }
    }
}
