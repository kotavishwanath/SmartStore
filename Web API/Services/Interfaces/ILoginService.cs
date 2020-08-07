using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using System.Threading.Tasks;

namespace smartStoreApi.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserResponse> Authenticate(LoginRequest loginRequest);
    }
}