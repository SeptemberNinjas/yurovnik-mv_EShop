using Core;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database
{
    internal class OrderDatabaseRepository : DatabaseContext, IRepository<Order>
    {
        public OrderDatabaseRepository(string connectionString) : base(connectionString) { }

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

            var infos = new List<OrderInfo>();

            while (reader.Read())
            {
                infos.Add(GetOrderInfo(reader));
            }

            var grouped = infos.GroupBy(i => i.OrderId);

            return infos.GroupBy(i => i.OrderId).Select(g =>
                new Order(g.Key, g.First().OrderStatus, g.Select(gg => gg.Line)));
        }

        private static IEnumerable<Order> GetOrders(IEnumerable<OrderInfo> infos) 
        {
            return infos.GroupBy(i => i.OrderId)
            .Select(g =>
                new Order(g.Key, g.First().OrderStatus, g.Select(gg => gg.Line)));
        }

        private OrderInfo GetOrderInfo(DbDataReader reader)
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

            return new OrderInfo
            {
                Line = new CartLine(item, reader.GetFieldValue<int>("count")),
                OrderId = reader.GetFieldValue<int>("id"),
                OrderStatus = (OrderStatus)reader.GetFieldValue<int>("status")
            };
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

        public int GetCount()
        {
            using var command = GetCommand(
                $"""
                select count(1) from "order";
                """);

            var result = command.ExecuteScalar();
            return int.TryParse(result?.ToString(), out var count) ? count : 0;
        }

        public async Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var commandText = $"""
                 select o.*, ol.*, c.type, c.name, c.price, COALESCE(s.amount, 0) as amount
                 from "order" o
                 join order_line ol on o.id = ol.order_id
                 join catalog c on ol.item_id = c.id
                 left join stock s on c.id = s.id
                """;

            var result = await ExecuteReaderListAsync(commandText, GetOrderInfo, cancellationToken);

            return GetOrders(result).ToArray();
        }

        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var commandText = $"""
                 select o.*, ol.*, c.type, c.name, c.price, COALESCE(s.amount, 0) as amount
                 from "order" o
                 join order_line ol on o.id = ol.order_id
                 join catalog c on ol.item_id = c.id
                 left join stock s on c.id = s.id
                 where o.id = {id};
                """;

            var result = await ExecuteReaderListAsync(commandText, GetOrderInfo, cancellationToken);
            return GetOrders(result).SingleOrDefault();
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var commandText = $"""
                select count(1) from "order";
                """;

            var result = await ExecuteReaderAsync(commandText, (reader) =>
            {
                return int.TryParse(reader[0]?.ToString(), out var count) ? count : 0;
            }, cancellationToken);

            return result;
        }

        public async Task<int> InsertAsync(Order item, CancellationToken cancellationToken)
        {
            using var command = GetCommand(
                $"""
                with inserted_id as (insert into "order"(status) values ({(int)item.Status}) returning id)
                insert into order_line(order_id, item_id, count) values
                {string.Join(',', item.Lines.Select(l => $"((select id from inserted_id),{l.ItemId}, {l.Count})"))}
                """);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Order item, CancellationToken cancellationToken)
        {
            using var command = GetCommand(
                $"""
                begin;
                delete from order_line where order_id = {item.Id};
                update order set status = {(int)item.Status} where id = {item.Id};
                inser into order_line(order_id, item_id, count) values
                {string.Join(',', item.Lines.Select(l => $"({item.Id},{l.ItemId},{l.Count})"))}
                """);

            await command.ExecuteNonQueryAsync();
        }
        readonly struct OrderInfo
        {
            public CartLine Line { get; init; }

            public int OrderId { get; init; }

            public OrderStatus OrderStatus { get; init; }
        }
    }
}
