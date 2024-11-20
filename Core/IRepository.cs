namespace Core
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        int Insert(T item);

        void Update(T item);

        Task InsertAsync();
        Task UpdateAsync();
    }
}
