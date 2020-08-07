using System;

namespace smartStoreApi.Helpers
{
    public static class BCryptPasswordHashHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }
            if (providedPassword == null)
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}