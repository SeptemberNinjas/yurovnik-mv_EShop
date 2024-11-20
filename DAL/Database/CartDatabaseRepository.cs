using Core;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<IReadOnlyCollection<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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

        public Task<Cart?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetById(default) is null ? 0 : 1;
        }

        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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

        public Task InsertAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        private CartLine GetCartLine(NpgsqlDataReader reader)
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
