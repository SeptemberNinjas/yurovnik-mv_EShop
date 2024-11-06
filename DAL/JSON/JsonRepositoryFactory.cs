using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.JSON
{
    internal class JsonRepositoryFactory : RepositoryFactory
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
