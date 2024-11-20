using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IReadOnlyRepository<T>
    {
        IReadOnlyCollection<T> GetAll();

        int GetCount();

        T? GetById(int id);

        Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(CancellationToken cancellationToken = default);

        Task<T?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
    }
}
