﻿using Npgsql;
using System.Data;
using System.Data.Common;

namespace DAL
{
    internal class DatabaseContext : IDisposable
    {
        private readonly string _connectionString;

        private NpgsqlConnection? _connection;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public NpgsqlConnection GetConnection() 
        {
            if (_connection is not null && _connection.State == ConnectionState.Open) 
                return _connection;

            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();

            return _connection;
        }

        private async Task<NpgsqlConnection> GetConnectionAsync() 
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                return _connection;

            _connection = new NpgsqlConnection(_connectionString);
            await _connection.OpenAsync();

            return _connection;
        }

        public NpgsqlCommand GetCommand(string text)
        {
            return new NpgsqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = text
            };
        }

        protected async Task<List<T>> ExecuteReaderListAsync<T>(string commandText, Func<DbDataReader, T> binging, CancellationToken cancellationToken)
        {
            using var connection = await GetConnectionAsync();

            var command = GetCommand(commandText);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var result = new List<T>();

            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(binging(reader));
            }

            return result;
        }

        protected async Task<T?> ExecuteReaderAsync<T>(string commandText, Func<DbDataReader, T> binding, CancellationToken cancellationToken)
        {
            using var connection = await GetConnectionAsync();

            var command = GetCommand(commandText);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
                return binding(reader);


            return default;
        }
    }
}
