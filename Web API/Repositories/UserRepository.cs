using Dapper;
using smartStoreApi.Common;
using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using smartStoreApi.Repositories.Interfaces;
using smartStoreApi.Services.Interfaces;
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
                ,
                    userRequest.LastName
                ,
                    userRequest.Gender
                ,
                    userRequest.Email
                ,
                    userRequest.Password
                ,
                    userRequest.Phone
                ,
                    userRequest.Address1
                ,
                    userRequest.Address2
                ,
                    userRequest.PostCode
                ,
                    userRequest.County
                ,
                    userRequest.Country
                });
                transaction.Complete();
                return rowsAffected > 0 ? true : false;
            }
        }

        public async Task<UserProductResponse> GetUserProductsAsync(int userId)
        {
            using (var databaseConnection = _dbConnectionFactory.GetConnection())
            {
                return new UserProductResponse
                {
                    ProductResponse = (await databaseConnection.QueryAsync<ProductResponse>(MySQLQueries.GetProductsQuery)).ToList(),
                    UserViewedProduct = (await databaseConnection.QueryAsync<UserViewedProduct>(MySQLQueries.GetUserViewedProductsQuery, new { userId })).ToList()
                };
            }
        }

        public async Task<ProductDetailResponse> GetProductDetailsAsync(int productId, int categoryId, int userId)
        {
            using (var databaseConnection = _dbConnectionFactory.GetConnection())
            {
                
                var result = await databaseConnection.QueryFirstAsync<ProductDetailResponse>(MySQLQueries.GetProductDetailsQuery, new { productId });
                var resp = await databaseConnection.QueryFirstOrDefaultAsync<ProductDetailResponse>(MySQLQueries.GetUserViewedProductByProductId, new 
               { 
                    userId, 
                    productId 
                });
                if (resp == null)
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var rowsAffected = await databaseConnection.ExecuteAsync(MySQLQueries.InsertUserViewedProductQuery, new
                        {
                            userId,
                            productId
                        });
                        transaction.Complete();
                    }
                }
                else
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var rowsAffected = await databaseConnection.ExecuteAsync(MySQLQueries.UpdateUserViewedProductQuery, new
                        {
                            userId,
                            productId
                        });
                        transaction.Complete();
                    }
                }
                result.SuggestedProductResponse = (await databaseConnection.QueryAsync<SuggestedProductResponse>(MySQLQueries.GetSuggestedProductsQuery, new { categoryId })).ToList();
                return result;
            }
        }
    }
}