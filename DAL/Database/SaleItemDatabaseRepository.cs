using Core;
using System.Data;
using System.Data.Common;

namespace DAL.Database
{
    internal class SaleItemDatabaseRepository : DatabaseContext, IReadOnlyRepository<SaleItem>
    {
        public SaleItemDatabaseRepository(string connectionString) : base(connectionString) {}

        public IReadOnlyCollection<SaleItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public SaleItem? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<SaleItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var commandText = @"select c.*, COALESCE(s.amount, 0) as amount
                    from catalog c
                        left join stock s on c.id = s.id;";
            
            var result = await ExecuteReaderListAsync(commandText, GetSaleItem, cancellationToken);
            return result;
        }



        public async Task<SaleItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var commandText = $@"select c.*, COALESCE(s.amount, 0) as amount 
                    from catalog c
                        left join stock s on c.id = s.id
                    where c.Id = {id};";

            var result = await ExecuteReaderAsync(commandText, GetSaleItem, cancellationToken);
            return result;
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var commandText = "select count(1) as count from catalog where type = 1;";

            var result = await ExecuteReaderAsync(commandText, (reader) => { 
                return int.TryParse(reader[0]?.ToString(), out int result) ? result : 0;
            }, cancellationToken);
            return result;
        }

        private static SaleItem GetSaleItem(DbDataReader reader)
        {            
            var type = (ItemTypes)reader.GetFieldValue<int>("type");
            var id = reader.GetFieldValue<int>("id");
            var name = reader.GetFieldValue<string>("name");
            var price = reader.GetFieldValue<decimal>("price");

            return type switch
            {
                ItemTypes.Product => new Product(id, name, price,
                    reader.GetFieldValue<int>("amount")),
                ItemTypes.Service => new Service(id, name, price),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
