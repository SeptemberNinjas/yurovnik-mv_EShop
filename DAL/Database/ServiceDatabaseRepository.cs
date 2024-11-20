using Core;
using Npgsql;
using System.Data;

namespace DAL.Database
{
    internal class ServiceDatabaseRepository : DatabaseContext, IRepository<Service>
    {
        public ServiceDatabaseRepository(string connectionString) : base(connectionString) { }

        public void Clear()
        {
            using var command = GetCommand("delete * from catalog where type = 2;");
            command.ExecuteNonQuery();
        }

        public IReadOnlyCollection<Service> GetAll()
        {
            using var command = GetCommand(
                @"select *
                    from catalog
                    where type = 2;");

            using var reader = command.ExecuteReader();
            var result = new List<Service>();
            while (reader.Read())
            {
                result.Add(GetService(reader));
            }

            return result;
        }

        public Service? GetById(int id)
        {
            using var command = GetCommand(
                 $@"select *
                    from catalog 
                    where type = 2 and id = {id};");

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return GetService(reader);
            }

            return null;
        }

        public int GetCount()
        {
            return 1;
        }

        public int Insert(Service item)
        {
            using var command = GetCommand(
                    $@"insert into catalog (id, name, price, type)
                    values
                    ({item.Id}, {item.Name}, {item.Price}, {item.ItemType});");

            return command.ExecuteNonQuery();
        }

        public static Service GetService(NpgsqlDataReader reader)
        {
            return new Service(
                    reader.GetFieldValue<int>("id"),
                    reader.GetFieldValue<string>("name"),
                    reader.GetFieldValue<decimal>("price"));
        }

        public void Update(Service item)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Service>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Service?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
