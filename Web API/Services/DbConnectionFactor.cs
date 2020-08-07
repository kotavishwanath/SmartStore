using smartStoreApi.Services.Interfaces;
using System.Data;

namespace smartStoreApi.Services
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString) => _connectionString = connectionString;

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_connectionString);
        }
    }
}