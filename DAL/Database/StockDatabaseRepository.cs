using Core;
using System.Data;
using System.Data.Common;

namespace DAL.Database
{
    internal class StockDatabaseRepository : DatabaseContext, IRepository<Stock>
    {
        public StockDatabaseRepository(string connectionString) : base(connectionString)
        {
        }

        public IReadOnlyCollection<Stock> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Stock>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var commandText = "select * from stock;";
            var result = await ExecuteReaderListAsync(commandText, GetStock, cancellationToken);

            return result;
        }

        public Stock? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Stock?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var commandText = $@"select * from stock where id = {id};";
            var result = await ExecuteReaderAsync(commandText, GetStock, cancellationToken);
            return result;

        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var commandText = "select count(1) from stock;";
            var result = await ExecuteReaderAsync(commandText, (reader) => 
            {
                return int.TryParse(reader[0]?.ToString(), out var result) ? result : 0;
            }, cancellationToken);

            return result;
        }

        public int Insert(Stock item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertAsync(Stock item, CancellationToken cancellationToken)
        {
            var commandText = $"insert into stock(id, amount) values ({item.ItemId}, {item.Amount});";
            var result = await ExecuteReaderAsync(commandText, (reader) => 
            { 
                return int.TryParse(reader[0]?.ToString(), out int count) ? count : 0;
            }, cancellationToken);

            return result;
        }

        public void Update(Stock item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Stock item, CancellationToken cancellationToken)
        {
            using var command = GetCommand($"update stock set amount = {item.Amount} where id = {item.ItemId};");
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        private static Stock GetStock(DbDataReader reader)
        {
            return new Stock
            {
                ItemId = reader.GetFieldValue<int>("id"),
                Amount = reader.GetFieldValue<int>("amount")
            };
        }
    }
}
