using Dapper;
using smartStoreApi.Common;
using smartStoreApi.Repositories.Interfaces;
using smartStoreApi.Services.Interfaces;
using System.Threading.Tasks;
using System.Transactions;

namespace smartStoreApi.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public LoginRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> UpdatePasswordAsync(int id, string password)
        {
            using (var databaseConnection = _dbConnectionFactory.GetConnection())
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var rowsAffected = await databaseConnection.ExecuteAsync(MySQLQueries.ChangeUserPasswordQuery, new
                    {
                        id,
                        password
                    });
                    transaction.Complete();
                    return rowsAffected;
                }
            }
        }
    }
}