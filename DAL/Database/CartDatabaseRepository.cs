using Core;
using Npgsql;
using System.Data;
using System.Data.Common;


namespace DAL.Database
{
    internal class CartDatabaseRepository : DatabaseContext, IRepository<Cart>
    {
        public CartDatabaseRepository(string connectionString) : base(connectionString){}

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Cart> GetAll()
        {
            var cart = GetById(default);
            return cart is null? [] : [cart];
        }

        public Cart? GetById(int id)
        {
            using var command = GetCommand(
                $"""
                select c.*, s.*, cl.count
                from cart_line cl
                join catalog c on cl.item_id = c.id
                left join stock s on c.id = s.id
                """);

            using var reader = command.ExecuteReader();
            if(!reader.HasRows)
                return null;

            var lines = new List<CartLine>();
            while (reader.Read())
            {
                lines.Add(GetCartLine(reader));
            }
            if (lines.Count > 0)
            {
                return new Cart(lines);
            }

            return null;
        }

        public int GetCount()
        {
            return GetById(default) is null ? 0 : 1;
        }

        public int Insert(Cart item)
        {
            using var command = item.Lines.Count > 0
                ? GetCommand(
                    $"""
                    truncate cart_line;
                    insert into cart_line(item_id, count)
                    values 
                    {string.Join(',', item.Lines.Select(cl => $"({cl.ItemId}, {cl.Count})"))};
                    """)
                : GetCommand(
                    "truncate cart_line;"
                    );
            return command.ExecuteNonQuery();
        }

        public void Update(Cart item)
        {
            Insert(item);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {           
            var cart = await GetByIdAsync(default, cancellationToken);
            return cart is null ? 0 : 1;

        }

        public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var command =
                $"""
                select c.*, s.*, cl.count
                from cart_line cl
                join catalog c on cl.item_id = c.id
                left join stock s on c.id = s.id
                """;

            var result = await ExecuteReaderListAsync(command, GetCartLine, cancellationToken);

            if (result.Count != 0)
            {
                return new Cart(result);
            }

            return null;
        }

        public async Task<IReadOnlyCollection<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var cart = await GetByIdAsync(default, cancellationToken);
            return cart is null ? [] : [cart];
        }

        public async Task<int> InsertAsync(Cart item, CancellationToken cancellationToken)
        {
            using var command = item.Lines.Count > 0
                ? GetCommand(
                    $"""
                    truncate cart_line;
                    insert into cart_line(item_id, count)
                    values 
                    {string.Join(',', item.Lines.Select(cl => $"({cl.ItemId}, {cl.Count})"))};
                    """)
                : GetCommand(
                    "truncate cart_line;"
                    );
            return await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Cart item, CancellationToken cancellationToken)
        {
            await InsertAsync(item, cancellationToken);
        }

        private static CartLine GetCartLine(DbDataReader reader)
        {
            var itemType = (ItemTypes)reader.GetFieldValue<int>("type");
            SaleItem item = itemType == ItemTypes.Product
                ? new Product(reader.GetFieldValue<int>("id"),
                    reader.GetFieldValue<string>("name"),
                    reader.GetFieldValue<decimal>("price"),
                    reader.GetFieldValue<int>("amount"))
                : new Service(reader.GetFieldValue<int>("id"),
                    reader.GetFieldValue<string>("name"),
                    reader.GetFieldValue<decimal>("price"));

            return new CartLine(item, reader.GetFieldValue<int>("count"));
        }
    }
}
