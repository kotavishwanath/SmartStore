using smartStoreApi.Helpers;
using smartStoreApi.Services.Interfaces;

namespace smartStoreApi.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(string password)
        {
            return BCryptPasswordHashHelper.HashPassword(password);
        }

        public bool Verify(string hashedPassword, string providedPassword)
        {
            return BCryptPasswordHashHelper.VerifyHashedPassword(hashedPassword, providedPassword);
        }
    }
}