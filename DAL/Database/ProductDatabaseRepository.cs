using Core;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace DAL.Database
{
    internal class ProductDatabaseRepository : DatabaseContext, IRepository<Product>
    {
        public ProductDatabaseRepository(string connectionString) : base(connectionString) { }

        public void Clear()
        {
            using var command = GetCommand("delete * from catalog where type = 1;");
            command.ExecuteNonQuery();
        }

        public IReadOnlyCollection<Product> GetAll()
        {
            using var command = GetCommand(
                @"select c.*, COALESCE(s.amount, 0) as amount
                    from catalog c
                        left join stock s on c.id = s.id
                    where type = 1;");

            using var reader = command.ExecuteReader();
            var result = new List<Product>();
            while (reader.Read()) 
            {
                result.Add(GetProduct(reader));
            }

            return result;
        }

        public Product? GetById(int id)
        {
            using var command = GetCommand(
                 $@"select c.*, COALESCE(s.amount, 0) as amount 
                    from catalog c
                        left join stock s on c.id = s.id
                    where type = 1 and c.Id = {id};");

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return GetProduct(reader);
            }

            return null;
        }

        public int GetCount()
        {
            using var command = GetCommand(
                    "select count(1) as count from catalog where type = 1;"
                );

            var result = command.ExecuteScalar();

            return int.TryParse(result?.ToString(), out int count) ? count : 0;         
        }
        public int Insert(Product item)
        {
            using var command = GetCommand(
                    $@"insert into catalog (id, name, price, type)
                    values
                    ({item.Id}, {item.Name}, {item.Price}, {item.ItemType});
                    insert into stocks(id, amount) values ({item.Id}, {item.Stock});");            

            return command.ExecuteNonQuery();
        }

        public static Product GetProduct(DbDataReader reader)
        {
            return new Product(
                    reader.GetFieldValue<int>("id"),
                    reader.GetFieldValue<string>("name"),
                    reader.GetFieldValue<decimal>("price"),
                    reader.GetFieldValue<int>("amount"));
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var commandText = @"select c.*, COALESCE(s.amount, 0) as amount
                    from catalog c
                        left join stock s on c.id = s.id
                    where type = 1;";
            var result = await ExecuteReaderListAsync(commandText, GetProduct, cancellationToken);
            return result.ToArray();
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var commandText = "select count(1) as count from catalog where type = 1;";

            var result = await ExecuteReaderAsync(commandText, (reader) =>
            {
                return int.TryParse(reader[0]?.ToString(), out var count) ? count : 0;
            }, cancellationToken);

            return result;
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var commandText = $@"select c.*, COALESCE(s.amount, 0) as amount 
                    from catalog c
                        left join stock s on c.id = s.id
                    where type = 1 and c.Id = {id};";

            var result = await ExecuteReaderAsync(commandText, GetProduct, cancellationToken);
            return result;
        }
        
        public async Task<int> InsertAsync(Product item, CancellationToken cancellationToken)
        {
            var command = GetCommand($@"insert into catalog (id, name, price, type)
                    values
                    ({item.Id}, {item.Name}, {item.Price}, {item.ItemType});
                    insert into stocks(id, amount) values ({item.Id}, {item.Stock});");

            return await command.ExecuteNonQueryAsync();
        }

        public Task UpdateAsync(Product item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
