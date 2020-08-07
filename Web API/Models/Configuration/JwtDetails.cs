namespace smartStoreApi.Models.Configuration
{
    public class JwtDetails
    {
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}