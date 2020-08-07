using smartStoreApi.Models.Response;

namespace smartStoreApi.Services.Interfaces
{
    public interface IAuthenticateService
    {
        string GenerateSecurityToken(string email, int userId,string firstName);

        int GetUserId();

        AuthenticationResponse GetAuthenticatedUser();
    }
}