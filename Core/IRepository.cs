namespace Core
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        int Insert(T item);

        void Update(T item);

        Task<int> InsertAsync(T item, CancellationToken cancellationToken);
        Task UpdateAsync(T item, CancellationToken cancellationToken);
    }
}
