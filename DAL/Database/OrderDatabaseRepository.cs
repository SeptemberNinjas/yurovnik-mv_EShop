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
    internal class OrderDatabaseRepository : DatabaseContext, IRepository<Order>
    {
        public OrderDatabaseRepository(string connectionString) : base(connectionString) {}

        public IReadOnlyCollection<Order> GetAll()
        {
            using var command = GetCommand(
                $"""
                 select o.*, ol.*, c.type, c.name, c.price, COALESCE(s.amount, 0) as amount
                 from "order" o
                 join order_line ol on o.id = ol.order_id
                 join catalog c on ol.item_id = c.id
                 left join stock s on c.id = s.id
                """);

            return GetOrders(command).ToArray();
        }

        private IEnumerable<Order> GetOrders(NpgsqlCommand command)
        {
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
                return [];

            var infos = new List<(CartLine Line, int OrderId, OrderStatus orderStatus)>();

            while (reader.Read()) 
            { 
                infos.Add(GetOrderInfo(reader));
            }

            var grouped = infos.GroupBy(i => i.OrderId);

            return infos.GroupBy(i => i.OrderId).Select(g =>
                new Order(g.Key, g.First().orderStatus, g.Select(gg => gg.Line)));
        }

        private (CartLine Line, int OrderId, OrderStatus orderStatus) GetOrderInfo(NpgsqlDataReader reader)
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

            return (new CartLine(item, reader.GetFieldValue<int>("count")),
                reader.GetFieldValue<int>("id"),
                (OrderStatus)reader.GetFieldValue<int>("status"));
        }

        public Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Order? GetById(int id)
        {
            using var command = GetCommand(
                $"""
                 select o.*, ol.*, c.type, c.name, c.price, COALESCE(s.amount, 0) as amount
                 from "order" o
                 join order_line ol on o.id = ol.order_id
                 join catalog c on ol.item_id = c.id
                 left join stock s on c.id = s.id
                 where o.id = {id};
                """);

            return GetOrders(command).SingleOrDefault();
        }

        public Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            using var command = GetCommand(
                $"""
                select count(1) from "order";
                """);

            var result = command.ExecuteScalar();
            return int.TryParse(result?.ToString(), out var count) ? count : 0;
        }

        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public int Insert(Order item)
        {
            using var command = GetCommand(
                $"""
                with inserted_id as (insert into "order"(status) values ({(int)item.Status}) returning id)
                insert into order_line(order_id, item_id, count) values
                {string.Join(',', item.Lines.Select(l => $"((select id from inserted_id),{l.ItemId}, {l.Count})"))}
                """);

            return command.ExecuteNonQuery();
        }

        public Task InsertAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Order item)
        {
            using var command = GetCommand(
                $"""
                begin;
                delete from order_line where order_id = {item.Id};
                update order set status = {(int)item.Status} where id = {item.Id};
                inser into order_line(order_id, item_id, count) values
                {string.Join(',', item.Lines.Select(l => $"({item.Id},{l.ItemId},{l.Count})"))}
                """);

            command.ExecuteNonQuery();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
