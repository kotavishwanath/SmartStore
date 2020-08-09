using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using System.Threading.Tasks;

namespace smartStoreApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserByEmailAsync(string email);

        Task<bool> ChangeUserPasswordAsync(ChangeUserPasswordRequest changeUserPasswordRequest);

        Task<UserResponse> GetUserAsync();

        Task<string> SaveUserAsync(UserRequest userRequest);

        Task<UserProductResponse> GetUserProductsAsync(int userId);
        Task<ProductDetailResponse> GetProductDetailsAsync(int productId, int categoryId, int userId);
    }
}