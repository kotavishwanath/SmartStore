using Dapper;
using smartStoreApi.Common;
using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using smartStoreApi.Repositories.Interfaces;
using smartStoreApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace smartStoreApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<UserResponse> GetUserByEmailAsync(string email)
        {
            using (var databaseConnection = _dbConnectionFactory.GetConnection())
            {
                return await databaseConnection.QueryFirstOrDefaultAsync<UserResponse>(MySQLQueries.GetUserByEmailQuery, new { Email = email });
            }
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            using (var databaseConnection = _dbConnectionFactory.GetConnection())
            {
                return await databaseConnection.QueryFirstOrDefaultAsync<UserResponse>(MySQLQueries.GetUserByIdQuery, new { id });
            }
        }

        public async Task<bool> InsertUserAsync(UserRequest userRequest)
        {
            using (var databaseConnection = _dbConnectionFactory.GetConnection())
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var rowsAffected = await databaseConnection.ExecuteAsync(MySQLQueries.InsertUserQuery, new
                {
                 userRequest.FirstName
                ,userRequest.LastName
                ,userRequest.Gender
                ,userRequest.Email
                ,userRequest.Password
                ,userRequest.Phone
                ,userRequest.Address1
                ,userRequest.Address2
                ,userRequest.PostCode
                ,userRequest.County
                ,userRequest.Country
                });
                transaction.Complete();
                return rowsAffected > 0 ? true : false;
            }
        }
    }
}