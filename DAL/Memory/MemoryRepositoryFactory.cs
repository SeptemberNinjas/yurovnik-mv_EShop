using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Memory
{
    public class MemoryRepositoryFactory : RepositoryFactory
    {
        /// <summary>
        /// Создать Memory репозиторий для продкутов
        /// </summary>
        /// <returns></returns>
        public override IRepository<Product> CreateProductFactory()
        {
            return new ProductMemoryRepository();
        }
        /// <summary>
        /// Создать Memory репозиторий для услуг
        /// </summary>
        /// <returns></returns>
        public override IRepository<Service> CreateServiceFactory()
        {
            return new ServiceMemoryRepository();
        }
    }
}
