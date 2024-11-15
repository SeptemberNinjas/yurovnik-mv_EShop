namespace Core
{
    public interface IRepository<T>
    {
        IReadOnlyCollection<T> GetAll();
        int GetCount();
        T? GetById(int id);
    }
}
