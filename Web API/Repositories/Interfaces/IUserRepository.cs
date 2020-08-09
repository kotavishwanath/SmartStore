using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smartStoreApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponse> GetUserByEmailAsync(string email);

        Task<UserResponse> GetUserByIdAsync(int id);

        Task<bool> InsertUserAsync(UserRequest userRequest);

        Task<UserProductResponse> GetUserProductsAsync(int userId);

        Task<ProductDetailResponse> GetProductDetailsAsync(int productId, int categoryId, int userId);

    }
}